using FamilyApplication.DTOs;
using FamilyApplication.Models;

namespace FamilyApplication.IRepos
{
    public interface IShowAllMembersRepo
    {
        Task<IEnumerable<ShowAllMembersReponseDTO>> ShowAllMembersAsync();
    }
}
