using FamilyApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace FamilyApplication.DBContext
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
            
        }
        public DbSet <RegisterModels> RegisterModels { get; set; }
        public DbSet <MemberModels> MemberModels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MemberModels>()
                        .HasOne(m => m.Register)
                        .WithMany(r => r.Members)
                        .HasForeignKey(m => m.RegisterId)
                        .OnDelete(DeleteBehavior.NoAction);
            base.OnModelCreating(modelBuilder);
        }
    }

    
}
