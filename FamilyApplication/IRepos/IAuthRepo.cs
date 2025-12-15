using FamilyApplication.Models;

namespace FamilyApplication.IRepos
{
    public interface IAuthRepo
    {
        Task<RegisterModels?> Login(string username);
        Task<RegisterModels?> GetById(int id);
        Task<RegisterModels?> getdetails(string username, string phone);
        Task<bool> updatepassword(RegisterModels registerModels);
    }
}
