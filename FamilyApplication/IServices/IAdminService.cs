using FamilyApplication.DTOs;
using FamilyApplication.Models;

namespace FamilyApplication.IServices
{
    public interface IAdminService
    {
        Task<RegisterDTO?> CreateAdmin(RegisterDTO registerDTO);
        Task<UpdateProfileDTO?> UpdateAdmin(UpdateProfileDTO updateProfileDTO, int id);
        Task<List<RegisterResponseDTO?>> GetAllUser();
        Task<RegisterDTO?> ChangeUserStatus(int id, string status);
        Task<AdminResponceDTO> ShowAdminInfo();
    }
}
