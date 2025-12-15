using FamilyApplication.DBContext;
using FamilyApplication.DTOs;
using FamilyApplication.IRepos;
using FamilyApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace FamilyApplication.Repos
{
    public class ShowAllMembersRepo : IShowAllMembersRepo
    {
        private readonly ApplicationDBContext _context;

        public ShowAllMembersRepo(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ShowAllMembersReponseDTO>> ShowAllMembersAsync()
        {
           var user = await _context.RegisterModels.Where(s=>s.Status=="Approved" && s.Role=="FamilyHead").
                Include(m=>m.Members).ToListAsync();

            return user.Select(u => new ShowAllMembersReponseDTO
            {
                id = u.Id,
                ProfileImage = u.ProfileImage,
                FamilyHeadFName = u.Fname,
                FamilyHeadLName = u.Lname,
                Phone = u.Phone,
                Email = u.Email,
                Role = u.Role,
                Members = u.Members.Select(m => new MemberResponseDTO
                {
                    id = m.Id,
                    ImageUrl =  m.MemberImage,
                    Name = m.Name,
                    age = m.age,
                    Gender = m.Gender,
                    Relation = m.Relation
                }).ToList()
            }).ToList();
        }
    }
}
