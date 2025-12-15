using FamilyApplication.DTOs;
using FamilyApplication.IRepos;
using FamilyApplication.IServices;
using FamilyApplication.Models;

namespace FamilyApplication.Services
{
    public class ShowAllMembersService : IShowAllMembersService
    {
        private readonly IShowAllMembersRepo _showAllMembersRepo;

        public ShowAllMembersService(IShowAllMembersRepo showAllMembersRepo)
        {
            _showAllMembersRepo = showAllMembersRepo;
        }
        public async Task<IEnumerable<ShowAllMembersReponseDTO>> ShowAllMembersAsync()
        {
            return await _showAllMembersRepo.ShowAllMembersAsync();
        }
    }
}
