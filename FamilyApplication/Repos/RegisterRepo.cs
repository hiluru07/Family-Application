using FamilyApplication.DBContext;
using FamilyApplication.IRepos;
using FamilyApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace FamilyApplication.Repos
{
    public class RegisterRepo : IRegisterRepo
    {
        private readonly ApplicationDBContext _context;

        public RegisterRepo(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<RegisterModels> RegisterUser(RegisterModels model)
        {
            _context.RegisterModels.Add(model);
            await _context.SaveChangesAsync();
            return model;   
        }
        public async Task<RegisterModels?> Checkuser(string username)
        {
            return await _context.RegisterModels.FirstOrDefaultAsync(u=>u.Username == username);
        }
        public async Task<RegisterModels?> ViewUserById(int id)
        {
            var checkuser = await _context.RegisterModels.FindAsync(id);
            return checkuser;
        }

        public async Task UpdateUser(RegisterModels models)
        {
            _context.RegisterModels.Update(models);
            await _context.SaveChangesAsync();  
        }

        public async Task <IEnumerable<RegisterModels>> GetAllUser()
        {
           return await _context.RegisterModels.Where(u=>u.Role !="Admin").ToListAsync();
        }
        public async Task DeleteUser(int id)
        {
            var finduser = await _context.RegisterModels.FindAsync(id);
            if (finduser != null)
            {
                _context.RegisterModels.Remove(finduser);
                await _context.SaveChangesAsync();
            }
        }
     
    }
}
