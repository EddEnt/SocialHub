using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class DataContext : IdentityDbContext<AppUser>
    {
        public DbSet<Domain.Activity> Activities { get; set; }
        public DbSet<Domain.ActivityAttendee> ActivityAttendees { get; set; }
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ActivityAttendee>(x => x.HasKey(
                attendee => new { attendee.AppUserId, attendee.ActivityId }));

            builder.Entity<ActivityAttendee>()
                .HasOne(attendee => attendee.AppUser)
                .WithMany(user => user.Activities)
                .HasForeignKey(attendee => attendee.AppUserId);

            builder.Entity<ActivityAttendee>()
                .HasOne(attendee => attendee.Activity)
                .WithMany(activity => activity.Attendees)
                .HasForeignKey(attendee => attendee.ActivityId);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //For now, the database is stored in the project folder
            //This will be changed later
            optionsBuilder.UseSqlite("Data Source=social_hub.db");
        }

    }
}
