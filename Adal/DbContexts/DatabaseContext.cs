using Adal.Models;
using Core.CoreClass;
using Microsoft.EntityFrameworkCore;

namespace Adal.DbContexts
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }
        
        public DbSet<Users> Users { get; set; }
        public DbSet<City> City { get; set; }
        public DbSet<CaseLawyerMapping> CaseLawyerMapping { get; set; }
        public DbSet<CasePaymentMapping> CasePaymentMapping { get; set; }
        public DbSet<Cases> Cases { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<LawyerCategoryMapping> LawyerCategoryMapping { get; set; }
        public DbSet<LawyerTypes> LawyerTypes { get; set; }
        public DbSet<MediaGallery> MediaGallery { get; set; }
        public DbSet<Rating> Rating { get; set; }
        public DbSet<UserRole> UserRole { get; set; }

    }
}
