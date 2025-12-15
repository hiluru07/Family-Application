using FamilyApplication.DBContext;
using FamilyApplication.IRepos;
using FamilyApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace FamilyApplication.Repos
{
    public class AuthRepo : IAuthRepo
    {
        private readonly ApplicationDBContext _context;

        public AuthRepo(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<RegisterModels?> Login(string username)
        {
          return await _context.RegisterModels.FirstOrDefaultAsync(u=>u.Username == username);
        }
        public async Task<RegisterModels?> GetById(int id)
        {
            return await _context.RegisterModels.FindAsync(id);
        }

        public async Task<RegisterModels?> getdetails(string username, string phone)
        {
          return await _context.RegisterModels.FirstOrDefaultAsync(u=>u.Username == username && u.Phone == phone);
        }
        public async Task<bool> updatepassword(RegisterModels registerModels)
        {
            _context.RegisterModels.Update(registerModels);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
