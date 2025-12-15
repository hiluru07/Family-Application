using FamilyApplication.DTOs;

namespace FamilyApplication.IServices
{
    public interface IShowAllMembersService
    {
        Task<IEnumerable<ShowAllMembersReponseDTO>> ShowAllMembersAsync();
    }
}
