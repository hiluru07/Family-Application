using FamilyApplication.Models;

namespace FamilyApplication.IRepos
{
    public interface IMemberRepo
    {
        Task <MemberModels> AddMember(MemberModels memberModels);

        Task<bool> DeleteMember(int id);

        Task UpdateMember(MemberModels memberModels);

        Task <IEnumerable<MemberModels?>> GetMember(int userId);

        Task <MemberModels?> GetById(int id);   
    }
}
