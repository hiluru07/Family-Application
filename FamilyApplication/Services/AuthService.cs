using FamilyApplication.CommonServices;
using FamilyApplication.DTOs;
using FamilyApplication.IRepos;
using FamilyApplication.IServices;
using FamilyApplication.Models;

namespace FamilyApplication.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepo _authRepo;
        private readonly PasswordHasedService _passwordHasedService;

        public AuthService(IAuthRepo authRepo, PasswordHasedService passwordHasedService)
        {
            _authRepo = authRepo;
            _passwordHasedService = passwordHasedService;
        }

        public async Task<LoginResponseDTO?> Login(LoginDTO loginDTO)
        {
            var user = await _authRepo.Login(loginDTO.username);

            if(user == null || !_passwordHasedService.VerifyPasswordHashed(user.Password,loginDTO.password))
            {
                return null;
            }
            if (user.Status == "Pending")
            {
                return new LoginResponseDTO
                {
                    userId = user.Id,
                    username = loginDTO.username,
                    Role = user.Role,
                    status = user.Status,
                    Message = "wait for approval"
                };
            }
            return new LoginResponseDTO
            {
                userId = user.Id,
                username = user.Username,
                ProfileImage = user.ProfileImage,
                Fname = user.Fname,
                Lname = user.Lname,
                Phone = user.Phone,
                Email = user.Email,
                Role = user.Role,
                status = user.Status,
                Message = "Login Success"
            };
        }
        public async Task<int?> verifyuser(ForgetPassDTO forgetPassDTO)
        {
            var user = await _authRepo.getdetails(forgetPassDTO.Username, forgetPassDTO.Phone);
            if (user == null) return null;
            return user.Id;
        }
        public async Task <bool> updatepass(int id,UpdatePasswordDTO updateDTO)
        {
            var userid = await _authRepo.GetById(id);
            var user = await _authRepo.getdetails(updateDTO.Username,updateDTO.Phone);
            if (user == null) return false; 

            user.Password = _passwordHasedService.PasswordHased(updateDTO.Password);
            user.Cpassword = _passwordHasedService.PasswordHased(updateDTO.CPassword);

            return await _authRepo.updatepassword(user);
            
        }
    }
}
