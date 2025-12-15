using FamilyApplication.CommonServices;
using FamilyApplication.DTOs;
using FamilyApplication.IRepos;
using FamilyApplication.IServices;
using FamilyApplication.Models;
using System.Linq;

namespace FamilyApplication.Services
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepo _adminRepo;
        private readonly PasswordHasedService _passwordHasedService;
        private readonly string _imagePath;

        public AdminService(IAdminRepo adminRepo, PasswordHasedService passwordHasedService, IWebHostEnvironment env)
        {
            _adminRepo = adminRepo;
            _passwordHasedService = passwordHasedService;
            _imagePath = Path.Combine(env.WebRootPath!, "Images");

            if(!Directory.Exists(_imagePath))
                Directory.CreateDirectory(_imagePath);
        }

        public async Task<RegisterDTO?> CreateAdmin(RegisterDTO registerDTO)
        {
            var existAdmin = await _adminRepo.CheckAdmin();

            if (existAdmin) return null;

            var admin = new RegisterModels
            {
                Username = registerDTO.Username,
                Fname = registerDTO.Fname,
                Lname = registerDTO.Lname,
                Phone = registerDTO.Phone,
                Email = registerDTO.Email,
                Password = _passwordHasedService.PasswordHased(registerDTO.Password),
                Cpassword = _passwordHasedService.PasswordHased(registerDTO.Cpassword),
                Role ="Admin"
            };
            await _adminRepo.CreateAdmin(admin);
            return registerDTO;
        }

        public async Task<UpdateProfileDTO?> UpdateAdmin(UpdateProfileDTO updateProfileDTO, int id)
        {
            var admin = await _adminRepo.GetAdminById(id);
            if (admin == null) return null;

            if (!string.IsNullOrEmpty(admin.ProfileImage))
            {
                var old = Path.Combine(_imagePath, admin.ProfileImage);
                if(File.Exists(old)) 
                    File.Delete(old);
            }
            if(updateProfileDTO.ProfileImage != null)
            {
                string file = Guid.NewGuid() + Path.GetExtension(updateProfileDTO.ProfileImage.FileName);
                using var stream = new FileStream(Path.Combine(_imagePath, file), FileMode.Create);
                await updateProfileDTO.ProfileImage.CopyToAsync(stream);
                admin.ProfileImage = file;
            }
            admin.Username = updateProfileDTO.Username;
            admin.Fname = updateProfileDTO.Fname;
            admin.Lname = updateProfileDTO.Lname;
            admin.Phone = updateProfileDTO.Phone;
            admin.Email = updateProfileDTO.Email;
             
           await _adminRepo.UpdateAdmin(admin);
           return updateProfileDTO;
        }

        public async Task<List<RegisterResponseDTO>> GetAllUser() { 
            var status = await _adminRepo.ShowStatus(); 
            var user = await _adminRepo.GetAllUser(); 
            return user.Select(u => new RegisterResponseDTO 
            {  
                Id = u.Id, 
                ProfileImage = u.ProfileImage,
                Username = u.Username, 
                Fname = u.Fname, 
                Lname = u.Lname, 
                Phone = u.Phone, 
                Email = u.Email,
                Status = u.Status, 
                Role = u.Role, 
            }).ToList();
            }

        public async Task<RegisterDTO?> ChangeUserStatus(int id, string status)
        {
            var user = await _adminRepo.ChangeUserStatus(id, status);
            return new RegisterDTO
            {
                Status = user.Status
            };
        }
        public async Task<AdminResponceDTO> ShowAdminInfo()
        {
            var admin = await _adminRepo.ShowAdminInfo();
            return new AdminResponceDTO
            {
                Id = admin.Id,
                username = admin.Username,
                ProfileImage = admin.ProfileImage,
                Fname = admin.Fname,
                Lname = admin.Lname,
                Phone = admin.Phone,
                Email = admin.Email,
            };
        }
    }

}
