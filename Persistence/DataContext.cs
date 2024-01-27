using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class DataContext : IdentityDbContext<AppUser>
    {
        public DbSet<Domain.Activity> Activities { get; set; }
        public DataContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //For now, the database is stored in the project folder
            //This will be changed later
            optionsBuilder.UseSqlite("Data Source=social_hub.db");
        }

    }
}
