using FamilyApplication.DTOs;
using FamilyApplication.Models;

namespace FamilyApplication.IServices
{
    public interface IRegisterService
    {
        Task<RegisterDTO> RegisterUser(RegisterDTO registerDTO);
        Task<RegisterModels?> ViewUserById(int id);
        Task<IEnumerable<RegisterModels>> GetAllUser();
        Task<UpdateProfileDTO?> UpdateUser(UpdateProfileDTO updateProfileDTO , int id);
        Task<bool> DeleteUser(int id);
    }
}
