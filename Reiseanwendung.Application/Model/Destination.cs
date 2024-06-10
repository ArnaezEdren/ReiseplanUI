using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Reiseanwendung.Application.Model
{
    [Table("Destination")]
    public class Destination : IEntity<int>
    {
        public Guid Guid { get; private set; }
        public int Id { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public ICollection<Activity> Activities { get; set; } = new List<Activity>();
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
        public ICollection<Accommodation> Accommodations { get; set; } = new List<Accommodation>();
        public ICollection<Transportation> Transportations { get; set; } = new List<Transportation>();
        public int TravelPlanId { get; set; }  // Ändern Sie dies zu int
        public TravelPlan? TravelPlan { get; set; }  // Non-nullable setzen

        public Destination()
        {
            Guid = Guid.NewGuid();
        }

        public Destination(string city, string country) : this()
        {
            City = city;
            Country = country;
        }

        public void AddActivity(Activity activity)
        {
            if (activity == null) throw new ArgumentNullException(nameof(activity));
            Activities.Add(activity);
        }

        public void AddBooking(Booking booking)
        {
            if (booking == null) throw new ArgumentNullException(nameof(booking));
            Bookings.Add(booking);
        }

        public void AddAccommodation(Accommodation accommodation)
        {
            if (accommodation == null) throw new ArgumentNullException(nameof(accommodation));
            Accommodations.Add(accommodation);
        }

        public void AddTransportation(Transportation transportation)
        {
            if (transportation == null) throw new ArgumentNullException(nameof(transportation));
            Transportations.Add(transportation);
        }
    }
}
