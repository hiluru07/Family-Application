using FamilyApplication.DBContext;
using FamilyApplication.IRepos;
using FamilyApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace FamilyApplication.Repos
{
    public class AdminRepo : IAdminRepo
    {
        private readonly ApplicationDBContext _context;

        public AdminRepo(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<RegisterModels> CreateAdmin(RegisterModels model)
        {
             _context.RegisterModels.Add(model);
             await _context.SaveChangesAsync();
             return model;
        }
        public async Task UpdateAdmin(RegisterModels model)
        {
            _context.RegisterModels.Update(model);
            await _context.SaveChangesAsync();  
        }
        public async Task<RegisterModels?> GetAdminById(int id)
        {
            return await _context.RegisterModels
                .FirstOrDefaultAsync(u => u.Id == id && u.Role == "Admin");
        }

        public async Task<List<RegisterModels>> GetAllUser()
        {
          return await _context.RegisterModels.Where(u => u.Role != "Admin" && u.Status=="Pending").ToListAsync();
        }

        public async Task <List<RegisterModels>> ShowStatus()
        {
            return await _context.RegisterModels.Where(u => u.Status == "Pending").ToListAsync();
        }

        public async Task<RegisterModels?> GetUserById(int id)
        {
          var user = await _context.RegisterModels.FindAsync(id);
            return user;
        }

        public async Task<RegisterModels?> ChangeUserStatus(int id, string status)
        {
            var user = await _context.RegisterModels.FindAsync(id);
            if (user != null)
            {
                user.Status = status;
                await _context.SaveChangesAsync();
            }
            return user;
        } 
        public async Task<bool> CheckAdmin()
        {
            return await _context.RegisterModels.AnyAsync(u => u.Role == "Admin");
        }
        public async Task<RegisterModels> ShowAdminInfo()
        {
            return await _context.RegisterModels.FirstOrDefaultAsync(u => u.Role == "Admin");
        }
    }
}
