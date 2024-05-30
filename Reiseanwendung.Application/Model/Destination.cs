using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Reiseanwendung.Application.Model
{
    [Table("Destination")]
    public class Destination
    {
        public Guid Id { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public ICollection<Activity> Activities { get; set; } = new List<Activity>();
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
        public ICollection<Accommodation> Accommodations { get; set; } = new List<Accommodation>();
        public ICollection<Transportation> Transportations { get; set; } = new List<Transportation>();

        // Foreign key to TravelPlan
        public Guid TravelPlanId { get; set; }
        public TravelPlan? TravelPlan { get; set; }

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
