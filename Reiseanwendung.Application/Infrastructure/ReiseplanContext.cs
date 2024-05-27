using Bogus;
using Microsoft.EntityFrameworkCore;
using Reiseanwendung.Application.Model;
using System;

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
        public DbSet<Model.Person> Persons => Set<Model.Person>();
        public DbSet<Guide> Guides => Set<Guide>();
        public DbSet<Transportation> Transportations => Set<Transportation>();
        public DbSet<Traveler> Travelers => Set<Traveler>();
        public DbSet<TravelPlan> TravelPlans => Set<TravelPlan>();
        public DbSet<TravelPlanService> TravelPlanServices => Set<TravelPlanService>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
             modelBuilder.Entity<Model.Person>()
                .ToTable("Persons")
                .HasDiscriminator<string>("PersonType")
                .HasValue<Model.Person>("Person")
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
                optionsBuilder.UseSqlite("Data Source=Reiseplan.db");
            }
        }

        public void SeedData(ReiseplanContext context)
        {
            var faker = new Faker("de");

            var addresses = new Faker<Address>()
                .CustomInstantiator(f => new Address(
                    f.Address.StreetAddress(),
                    f.Address.City(),
                    f.Address.Country(),
                    f.Address.ZipCode()
                )).Generate(10);

            var accommodations = new Faker<Accommodation>()
                .CustomInstantiator(f => new Accommodation(
                    f.Company.CompanyName(),
                    addresses[f.Random.Int(0, 9)]
                )).Generate(10);

            var activities = new Faker<Activity>()
                .CustomInstantiator(f =>
                {
                    var startDateTime = f.Date.Future();
                    var endDateTime = startDateTime.AddHours(f.Random.Int(1, 5)); // Ensure endDateTime is after startDateTime
                    return new Activity(
                        f.Commerce.ProductName(),
                        f.Lorem.Sentence(),
                        startDateTime,
                        endDateTime
                    );
                }).Generate(10);

            var bookings = new Faker<Booking>()
                .CustomInstantiator(f => new Booking(
                    f.Finance.Amount(),
                    f.Date.Past()
                )).Generate(10);

            var destinations = new Faker<Destination>()
                .CustomInstantiator(f => new Destination
                {
                    City = f.Address.City(),
                    Country = f.Address.Country()
                }).Generate(10);

            var guides = new Faker<Guide>()
                .CustomInstantiator(f => new Guide(
                    f.Name.FullName(),
                    f.Random.Replace("######")
                )).Generate(10);

            var travelers = new Faker<Traveler>()
                .CustomInstantiator(f => new Traveler(
                    f.Name.FullName(),
                    f.Random.Replace("########")
                )).Generate(10);

            var travelPlans = new Faker<TravelPlan>()
                .CustomInstantiator(f => new TravelPlan(
                    f.Lorem.Word(),
                    f.Date.Past(),
                    f.Date.Future()
                )).Generate(10);

            var transportations = new Faker<Transportation>()
                .CustomInstantiator(f => new Transportation(
                    f.Vehicle.Type()
                )).Generate(10);

            // Assign activities and bookings to destinations
            for (int i = 0; i < 10; i++)
            {
                destinations[i].AddActivity(activities[i]);
                destinations[i].AddBooking(bookings[i]);
            }

            // Assign destinations and people to travel plans
            for (int i = 0; i < 10; i++)
            {
                travelPlans[i].AddDestination(destinations[i]);
                travelPlans[i].People.Add(guides[i]);
                travelPlans[i].People.Add(travelers[i]);
            }

            // Add entities to context
            context.Accommodations.AddRange(accommodations);
            context.Activities.AddRange(activities);
            context.Bookings.AddRange(bookings);
            context.Destinations.AddRange(destinations);
            context.Guides.AddRange(guides);
            context.Travelers.AddRange(travelers);
            context.Transportations.AddRange(transportations);
            context.TravelPlans.AddRange(travelPlans);

            context.SaveChanges();
        }
    }
}
