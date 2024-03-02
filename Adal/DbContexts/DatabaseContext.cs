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

    }
}
