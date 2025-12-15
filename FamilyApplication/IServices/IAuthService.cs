using FamilyApplication.DTOs;

namespace FamilyApplication.IServices
{
    public interface IAuthService
    {
        Task<LoginResponseDTO?> Login(LoginDTO loginDTO);
        Task<int?> verifyuser(ForgetPassDTO forgetPassDTO);
        Task<bool> updatepass(int id, UpdatePasswordDTO updateDTO);
    }
}
