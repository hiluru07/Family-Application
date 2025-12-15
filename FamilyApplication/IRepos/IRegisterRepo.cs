using FamilyApplication.Models;

namespace FamilyApplication.IRepos
{
    public interface IRegisterRepo
    {
        Task<RegisterModels> RegisterUser(RegisterModels model);
        Task<RegisterModels?> Checkuser(string username);
        Task<RegisterModels?> ViewUserById(int id);
        Task <IEnumerable<RegisterModels>> GetAllUser();
        Task UpdateUser(RegisterModels model);
        Task DeleteUser(int id);
    }
}
