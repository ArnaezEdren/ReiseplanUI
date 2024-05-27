using Microsoft.EntityFrameworkCore;
using Reiseanwendung.Application.Infrastructure;
using Reiseanwendung.Application.Model;
using System;
using System.Linq;
using Xunit;

namespace Reiseanwendung.Test
{
    public class ReiseplanContextTests
    {
        private DbContextOptions<ReiseplanContext> GetInMemoryOptions()
        {
            return new DbContextOptionsBuilder<ReiseplanContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
        }

        private void SeedData(ReiseplanContext context)
        {
            var address = new Address("Street 1", "City", "Country", "12345");
            var accommodation = new Accommodation("Hotel", address);
            var activity = new Activity("Hiking", "Mountain hiking", DateTime.Now.AddHours(1), DateTime.Now.AddHours(3));
            var booking = new Booking(100m, DateTime.Now);
            var destination = new Destination { City = "Paris", Country = "France" };
            destination.AddActivity(activity);
            destination.AddBooking(booking);
            var guide = new Guide("Guide Name", "License123");
            var traveler = new Traveler("Traveler Name", "Passport123");
            var travelPlan = new TravelPlan("Trip to Paris", DateTime.Now, DateTime.Now.AddDays(7));
            travelPlan.AddDestination(destination);
            travelPlan.People.Add(guide);
            travelPlan.People.Add(traveler);
            var transportation = new Transportation("Car");

            context.Accommodations.Add(accommodation);
            context.Activities.Add(activity);
            context.Bookings.Add(booking);
            context.Destinations.Add(destination);
            context.Guides.Add(guide);
            context.Travelers.Add(traveler);
            context.Transportations.Add(transportation);
            context.TravelPlans.Add(travelPlan);
            context.SaveChanges();
        }

        [Fact]
        public void CanInsertAccommodationIntoDatabase()
        {
            var options = GetInMemoryOptions();
            using (var context = new ReiseplanContext(options))
            {
                SeedData(context);
                var accommodation = context.Accommodations.First();
                Assert.NotNull(accommodation);
                Assert.Equal("Hotel", accommodation.Name);
            }
        }

        [Fact]
        public void CanInsertActivityIntoDatabase()
        {
            var options = GetInMemoryOptions();
            using (var context = new ReiseplanContext(options))
            {
                SeedData(context);
                var activity = context.Activities.First();
                Assert.NotNull(activity);
                Assert.Equal("Hiking", activity.Name);
            }
        }

        // Additional tests for other models...
    }
}
