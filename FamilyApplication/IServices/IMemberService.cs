using FamilyApplication.DTOs;
using FamilyApplication.Models;

namespace FamilyApplication.IServices
{
    public interface IMemberService
    {
        Task<MemberModels> AddSingleMember(MemberDTO dto, int userId);
        Task<List<MemberModels>> AddMultipleMembers(MemberCreateModel model);
        Task<bool> DeleteMember(int id);
        Task<MemberDTO?> UpdateMember(MemberDTO memberDTO, int id, string? removeImage);
        Task<IEnumerable<MemberResponseDTO?>> GetMember(int userId);
    }
}
