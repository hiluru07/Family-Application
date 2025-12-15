using FamilyApplication.Models;

namespace FamilyApplication.IRepos
{
    public interface IAdminRepo
    {
        Task<RegisterModels> CreateAdmin(RegisterModels model);
        Task UpdateAdmin(RegisterModels model);
        Task <RegisterModels?> GetAdminById(int id);
        Task<List<RegisterModels>> GetAllUser();
        Task<RegisterModels?> GetUserById(int id);
        Task<RegisterModels?> ChangeUserStatus(int id, string status);
        Task<bool> CheckAdmin();
        Task<List<RegisterModels>> ShowStatus();
        Task<RegisterModels> ShowAdminInfo();

    }
}
