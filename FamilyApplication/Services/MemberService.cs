using FamilyApplication.DTOs;
using FamilyApplication.IRepos;
using FamilyApplication.IServices;
using FamilyApplication.Models;

namespace FamilyApplication.Services
{
    public class MemberService : IMemberService
    {
        private readonly IMemberRepo _memberRepo;
        private readonly string _imagepath;
        private readonly string _defaultImage;

        public MemberService(IMemberRepo memberRepo, IWebHostEnvironment env)
        {
            _memberRepo = memberRepo;
            _imagepath = Path.Combine(env.WebRootPath, "Images");
            _defaultImage = Path.Combine(env.WebRootPath, "DefaultImage");

           
        }
        // 🔹 Save image helper (private)
        private async Task<string> SaveImage(IFormFile? image)
        {
            if (!Directory.Exists(_imagepath))
                Directory.CreateDirectory(_imagepath);

            if (image == null || image.Length == 0)
                return "DefaultImage.jpg";

            string fileName = Guid.NewGuid() + Path.GetExtension(image.FileName);
            string fullPath = Path.Combine(_imagepath, fileName);

            using var stream = new FileStream(fullPath, FileMode.Create);
            await image.CopyToAsync(stream);

            return fileName;
        }
        // 🔹 Single member add
        public async Task<MemberModels> AddSingleMember(MemberDTO dto, int userId)
        {
            var member = new MemberModels
            {
                Name = dto.Name,
                age = dto.age,
                Gender = dto.Gender,
                Relation = dto.Relation,
                RegisterId = userId,
                MemberImage = await SaveImage(dto.MemberImage)
            };

           return await _memberRepo.AddMember(member);
        }
        // 🔹 Multiple members add
        public async Task<List<MemberModels>> AddMultipleMembers(MemberCreateModel model)
        {
            var addedMembers = new List<MemberModels>();
            foreach (var dto in model.Members)
            {
                var member = new MemberModels
                {
                    Name = dto.Name,
                    age = dto.age,
                    Gender = dto.Gender,
                    Relation = dto.Relation,
                    MemberImage = await SaveImage(dto.MemberImage),
                    RegisterId = model.UserId

                };

                var addedMember = await _memberRepo.AddMember(member);
                addedMembers.Add(addedMember);
            }
            return addedMembers;
        }

        public async Task<bool> DeleteMember(int id)
        {
            var member = await _memberRepo.GetById(id);
            if (member == null) return false;
            if (!string.IsNullOrEmpty(member.MemberImage))
            {
                var filePath = Path.Combine(_imagepath, member.MemberImage);
                if (File.Exists(filePath))
                    File.Delete(filePath);
            }
            return await _memberRepo.DeleteMember(id);
        }
        public async Task<MemberDTO?> UpdateMember(MemberDTO memberDTO, int id, string? removeImage)
        {
            var member = await _memberRepo.GetById(id);
            if (member == null) return null;

            // ✔ update fields
            member.Name = memberDTO.Name;
            member.age = memberDTO.age;
            member.Gender = memberDTO.Gender;
            member.Relation = memberDTO.Relation;

            // ✔ DELETE IMAGE
            if (removeImage == "true")
            {
                if (!string.IsNullOrEmpty(member.MemberImage) && member.MemberImage != "DefaultImage.jpg")
                {
                    var oldPath = Path.Combine(_imagepath, member.MemberImage);
                    if (File.Exists(oldPath))
                        File.Delete(oldPath);
                }

                member.MemberImage = "DefaultImage.jpg";   
            }

            if (memberDTO.MemberImage != null)
            {
                string fileName = Guid.NewGuid() + Path.GetExtension(memberDTO.MemberImage.FileName);
                using var stream = new FileStream(Path.Combine(_imagepath, fileName), FileMode.Create);
                await memberDTO.MemberImage.CopyToAsync(stream);

                member.MemberImage = fileName;
            }

            await _memberRepo.UpdateMember(member);

            return memberDTO;
        }


        public async Task<IEnumerable<MemberResponseDTO?>> GetMember(int userId)
        {
            var member = await _memberRepo.GetMember(userId);
            return member.Select(m => new MemberResponseDTO
            {  
                id = m.Id,
                ImageUrl = m.MemberImage,
                Name = m.Name, 
                age = m.age, 
                Gender=m.Gender,
                Relation = m.Relation  
            }).ToList();


        }
    }
}
