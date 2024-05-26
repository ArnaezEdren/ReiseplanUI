using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Reiseanwendung.Application.Model;

namespace Reiseanwendung.Application.Infrastructure
{
    public class ReiseplanContext : DbContext
    {
        public ReiseplanContext(DbContextOptions<ReiseplanContext> options) : base(options)
        {
        }

        public DbSet<Accommodation> Accommodations => Set<Accommodation>();
        public DbSet<Activity> Activities => Set<Activity>();
        public DbSet<Booking> Bookings => Set<Booking>();
        public DbSet<Destination> Destinations => Set<Destination>();
        public DbSet<Guide> Guides => Set<Guide>();
        public DbSet<Person> Persons => Set<Person>();
        public DbSet<Person> People { get; set; } = null!;
        public DbSet<Transportation> Transportations => Set<Transportation>();
        public DbSet<Traveler> Travelers => Set<Traveler>();
        public DbSet<TravelPlan> TravelPlans => Set<TravelPlan>();
        public DbSet<TravelPlanService> TravelPlanServices => Set<TravelPlanService>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>()
                .HasDiscriminator<string>("PersonType")
                .HasValue<Guide>("Guide")
                .HasValue<Traveler>("Traveler");

            modelBuilder.Entity<Accommodation>()
                .OwnsOne(a => a.Address);

            modelBuilder.Entity<TravelPlan>().ToTable("TravelPlans");

            modelBuilder.Entity<TravelPlan>()
                .HasMany(tp => tp.Destinations)
                .WithOne()
                .HasForeignKey("TravelPlanId");

            modelBuilder.Entity<TravelPlan>()
                .HasMany(tp => tp.People)
                .WithMany(p => p.TravelPlans)
                .UsingEntity(j => j.ToTable("TravelPlanParticipants"));

            modelBuilder.Entity<TravelPlanService>()
                .HasKey(t => t.Id);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=ReiseplanDb;Trusted_Connection=True;");
            }
        }
    }
}
