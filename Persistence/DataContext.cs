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

        public DbSet<Domain.ActivityAttendee> ActivityAttendees { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Domain.ActivityAttendee>
                (entity => entity.HasKey(activityAttendee => 
                new { activityAttendee.AppUserId, activityAttendee.ActivityId }));

            builder.Entity<Domain.ActivityAttendee>()
                .HasOne(user => user.AppUser)
                .WithMany(userActivity => userActivity.Activities)
                .HasForeignKey(activityAttendeeID => activityAttendeeID.AppUserId);

            builder.Entity<Domain.ActivityAttendee>()
                .HasOne(activityAttendee => activityAttendee.Activity)
                .WithMany(activity => activity.Attendees)
                .HasForeignKey(activityAttendeeID => activityAttendeeID.ActivityId);

        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //For now, the database is stored in the project folder
            //This will be changed later
            optionsBuilder.UseSqlite("Data Source=social_hub.db");
        }

    }
}
