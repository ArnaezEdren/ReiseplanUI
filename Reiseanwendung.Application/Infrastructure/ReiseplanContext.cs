using Bogus;
using Microsoft.EntityFrameworkCore;
using Reiseanwendung.Application.Model;
using System;
using System.Linq;

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

            modelBuilder.Entity<Destination>()
                .HasMany(d => d.Accommodations)
                .WithOne()
                .HasForeignKey("DestinationId");

            modelBuilder.Entity<Activity>()
                .HasMany(a => a.Bookings)
                .WithOne()
                .HasForeignKey("ActivityId");

            modelBuilder.Entity<Accommodation>()
                .HasMany(a => a.Bookings)
                .WithOne()
                .HasForeignKey("AccommodationId");

            modelBuilder.Entity<Transportation>()
                .HasDiscriminator<string>("TransportType")
                .HasValue<Flight>("Flight")
                .HasValue<Bus>("Bus")
                .HasValue<Train>("Train");

            modelBuilder.Entity<Destination>()
                .HasMany(d => d.Transportations)
                .WithOne()
                .HasForeignKey("DestinationId");

            modelBuilder.Entity<TravelPlan>()
                .HasMany(tp => tp.Destinations)
                .WithOne(d => d.TravelPlan)
                .HasForeignKey(d => d.TravelPlanId);

            modelBuilder.Entity<TravelPlan>()
                .ToTable("TravelPlans");

            modelBuilder.Entity<TravelPlanService>()
                .HasKey(t => t.Id);

            modelBuilder.Entity<TravelPlan>()
      .HasMany(tp => tp.Destinations)
      .WithOne(d => d.TravelPlan)
      .HasForeignKey(d => d.TravelPlanId)
      .OnDelete(DeleteBehavior.Cascade); // Cascade delete

            modelBuilder.Entity<Destination>()
                .HasMany(d => d.Activities)
                .WithOne()
                .HasForeignKey("DestinationId")
                .OnDelete(DeleteBehavior.Cascade); // Cascade delete

            modelBuilder.Entity<Destination>()
                .HasMany(d => d.Accommodations)
                .WithOne()
                .HasForeignKey("DestinationId")
                .OnDelete(DeleteBehavior.Cascade); // Cascade delete

            modelBuilder.Entity<Destination>()
                .HasMany(d => d.Transportations)
                .WithOne()
                .HasForeignKey("DestinationId")
                .OnDelete(DeleteBehavior.Cascade); // Cascade delete

            modelBuilder.Entity<Activity>()
                .HasMany(a => a.Bookings)
                .WithOne()
                .HasForeignKey("ActivityId")
                .OnDelete(DeleteBehavior.Cascade); // Cascade delete

            modelBuilder.Entity<Accommodation>()
                .HasMany(a => a.Bookings)
                .WithOne()
                .HasForeignKey("AccommodationId")
                .OnDelete(DeleteBehavior.Cascade); // Cascade delete
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
                )).Generate(20); // Generate 20 addresses instead of 10

            var accommodations = new Faker<Accommodation>()
                .CustomInstantiator(f => new Accommodation(
                    f.Company.CompanyName(),
                    addresses[f.Random.Int(0, 19)]
                )).Generate(20); // Generate 20 accommodations instead of 10

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
                }).Generate(40); // Generate 40 activities instead of 20

            var bookings = new Faker<Booking>()
                .CustomInstantiator(f => new Booking(
                    f.Finance.Amount(),
                    f.Date.Past(),
                    f.Random.Replace("###-###-###") // Generate a random booking number
                )).Generate(40); // Generate 40 bookings instead of 20

            var transportations = new Faker<Transportation>()
                .CustomInstantiator(f => new Flight(
                    f.Random.Replace("###-###-###"), // Generate a random booking number
                    f.Random.Bool(), // Randomly determine if it's round-trip
                    f.Finance.Amount(50, 500) // Random cost between 50 and 500
                )).Generate(10).Cast<Transportation>()
                .Concat(new Faker<Transportation>()
                    .CustomInstantiator(f => new Bus(
                        f.Random.Replace("###-###-###"), // Generate a random booking number
                        f.Random.Bool(), // Randomly determine if it's round-trip
                        f.Finance.Amount(10, 100) // Random cost between 10 and 100
                    )).Generate(10))
                .Concat(new Faker<Transportation>()
                    .CustomInstantiator(f => new Train(
                        f.Random.Replace("###-###-###"), // Generate a random booking number
                        f.Random.Bool(), // Randomly determine if it's round-trip
                        f.Finance.Amount(20, 200) // Random cost between 20 and 200
                    )).Generate(10))
                .ToList();

            var destinations = new Faker<Destination>()
                .CustomInstantiator(f => new Destination
                {
                    City = f.Address.City(),
                    Country = f.Address.Country()
                }).Generate(20); // Generate 20 destinations instead of 10

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

            // Assign activities, accommodations with bookings, and transportations to destinations
            for (int i = 0; i < 20; i++)
            {
                destinations[i].AddActivity(activities[i]);
                destinations[i].AddAccommodation(accommodations[i]);
                destinations[i].AddTransportation(transportations[i % transportations.Count]);
                accommodations[i].Bookings.Add(bookings[i]);
                activities[i].Bookings.Add(bookings[i]);
                // Assign additional activities and bookings
                destinations[i].AddActivity(activities[(i + 20) % activities.Count]);
                destinations[i].AddTransportation(transportations[(i + 20) % transportations.Count]);
                activities[(i + 20) % activities.Count].Bookings.Add(bookings[(i + 20) % bookings.Count]);
            }

            // Assign destinations and people to travel plans
            for (int i = 0; i < 10; i++)
            {
                travelPlans[i].AddDestination(destinations[i]);
                travelPlans[i].AddDestination(destinations[(i + 10) % destinations.Count]); // Adding additional destination
                travelPlans[i].People.Add(guides[i]);
                travelPlans[i].People.Add(travelers[i]);
            }

            // Add entities to context
            context.Accommodations.AddRange(accommodations);
            context.Activities.AddRange(activities);
            context.Bookings.AddRange(bookings);
            context.Transportations.AddRange(transportations);
            context.Destinations.AddRange(destinations);
            context.Guides.AddRange(guides);
            context.Travelers.AddRange(travelers);
            context.TravelPlans.AddRange(travelPlans);

            context.SaveChanges();
        }



    }
}
