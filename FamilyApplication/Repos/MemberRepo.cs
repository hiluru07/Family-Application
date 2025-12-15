using FamilyApplication.DBContext;
using FamilyApplication.IRepos;
using FamilyApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace FamilyApplication.Repos
{
    public class MemberRepo : IMemberRepo
    {
        private readonly ApplicationDBContext _context;

        public MemberRepo(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<MemberModels> AddMember(MemberModels memberModels)
        {
              _context.MemberModels.Add(memberModels); 
              await _context.SaveChangesAsync();
            return memberModels;    
        }
        public async Task<bool> DeleteMember(int id)
        {
            var member = await _context.MemberModels.FindAsync(id);

            if (member == null) return false; 
            
                _context.MemberModels.Remove(member);
                await _context.SaveChangesAsync();
                return true;
        }
        public async Task UpdateMember(MemberModels memberModels)
        {
            _context.MemberModels.Update(memberModels);
            await _context.SaveChangesAsync();
        }
        public async Task <IEnumerable<MemberModels?>> GetMember(int userId)
        {
            var member = await _context.MemberModels.Where(m=>m.RegisterId==userId).ToListAsync();
            return member;
        }

        public async Task<MemberModels?> GetById(int id)
        {
            return await _context.MemberModels.FindAsync(id);
        }
    }
}
