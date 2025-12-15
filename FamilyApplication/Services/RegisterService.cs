using FamilyApplication.CommonServices;
using FamilyApplication.DTOs;
using FamilyApplication.IRepos;
using FamilyApplication.IServices;
using FamilyApplication.Models;

namespace FamilyApplication.Services
{
    public class RegisterService : IRegisterService
    {
        private readonly IRegisterRepo _registerRepo;
        private readonly PasswordHasedService _passwordHasedService;
        private readonly string _imagePath;

        public RegisterService(IRegisterRepo registerRepo, PasswordHasedService passwordHasedService, IWebHostEnvironment env)
        {
            _registerRepo = registerRepo;
            _passwordHasedService = passwordHasedService;
            _imagePath = Path.Combine(env.WebRootPath!, "Images");

            if(!Directory.Exists(_imagePath))
                Directory.CreateDirectory(_imagePath);
        }

        public async Task<RegisterDTO> RegisterUser(RegisterDTO registerDTO)
        {
            var user = await _registerRepo.Checkuser(registerDTO.Username);
            if (user != null)
            {
                return null;
            } 
            string fileName = string.Empty;


            if (registerDTO.ProfileImage != null)
            {
                //var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images");

                fileName = Guid.NewGuid() + Path.GetExtension(registerDTO.ProfileImage.FileName);

                using var stream = new FileStream(Path.Combine(_imagePath, fileName), FileMode.Create);

                await registerDTO.ProfileImage.CopyToAsync(stream);
                
            }
            var register = new RegisterModels
            {
                Username = registerDTO.Username,
                ProfileImage = fileName,
                Fname = registerDTO.Fname,
                Lname = registerDTO.Lname,
                Phone = registerDTO.Phone,
                Email = string.IsNullOrWhiteSpace(registerDTO.Email) ? "Not Available" : registerDTO.Email,
                Password = _passwordHasedService.PasswordHased(registerDTO.Password),
                Cpassword = _passwordHasedService.PasswordHased(registerDTO.Cpassword),
                Role = "FamilyHead",
                Status = "Pending",
            };
            await _registerRepo.RegisterUser(register);
            return registerDTO;
        }

        public async Task<RegisterModels?> ViewUserById(int id)
        {
            var user = await _registerRepo.ViewUserById(id);
            if (user == null) return null;
            user.ProfileImage = user.ProfileImage;
            return user;
        }

        public async Task<IEnumerable<RegisterModels>> GetAllUser()
        {
            var user = await _registerRepo.GetAllUser();
            foreach(var users in user)
            {
                if(!string.IsNullOrEmpty(users.ProfileImage))
                {
                    users.ProfileImage = users.ProfileImage;
                }
            }
            return user;
        }

        public async Task<UpdateProfileDTO?> UpdateUser(UpdateProfileDTO updateProfileDTO, int id)
        {
            var user = await _registerRepo.ViewUserById(id);
            if (user == null) return null;

            if (updateProfileDTO.ProfileImage != null)
            {
                if (!string.IsNullOrEmpty(user.ProfileImage))
                {
                    var old = Path.Combine(_imagePath, user.ProfileImage);
                    if (File.Exists(old)) File.Delete(old);
                }

                string fileName = Guid.NewGuid() + Path.GetExtension(updateProfileDTO.ProfileImage.FileName);
                using var stream = new FileStream(Path.Combine(_imagePath, fileName), FileMode.Create);
                await updateProfileDTO.ProfileImage.CopyToAsync(stream);
                user.ProfileImage = fileName;
            }

            user.Username = updateProfileDTO.Username;
            user.Fname = updateProfileDTO.Fname;
            user.Lname = updateProfileDTO.Lname;
            user.Phone = updateProfileDTO.Phone;
            user.Email = updateProfileDTO.Email;

            await _registerRepo.UpdateUser(user);
            return updateProfileDTO;
        }


        public async Task<bool> DeleteUser(int id)
        {
            var user = await _registerRepo.ViewUserById(id);
            if (user == null) return false;

            if (!string.IsNullOrEmpty(user.ProfileImage))
            {
                var filePath = Path.Combine(_imagePath, user.ProfileImage);
                if (File.Exists(filePath))
                    File.Delete(filePath);
            }

            await _registerRepo.DeleteUser(id);
            return true;
        }
    }
}
