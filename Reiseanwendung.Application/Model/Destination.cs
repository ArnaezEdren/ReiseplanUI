using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Reiseanwendung.Application.Model
{
    [Table("Destination")]
    public class Destination
    {
        private readonly List<Activity> _activities = new List<Activity>();
        private readonly List<Accommodation> _accommodations = new List<Accommodation>();
        private readonly List<Transportation> _transportations = new List<Transportation>();

        public Guid Id { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }

        public IReadOnlyCollection<Activity> Activities => _activities.AsReadOnly();
        public IReadOnlyCollection<Accommodation> Accommodations => _accommodations.AsReadOnly();
        public IReadOnlyCollection<Transportation> Transportations => _transportations.AsReadOnly();

        public void AddActivity(Activity activity)
        {
            if (activity == null)
                throw new ArgumentNullException(nameof(activity));
            _activities.Add(activity);
        }

        public void AddAccommodation(Accommodation accommodation)
        {
            if (accommodation == null)
                throw new ArgumentNullException(nameof(accommodation));
            _accommodations.Add(accommodation);
        }

        public void AddTransportation(Transportation transportation)
        {
            if (transportation == null)
                throw new ArgumentNullException(nameof(transportation));
            _transportations.Add(transportation);
        }

        public decimal CalculateAverageBookingCost()
        {
            var allBookings = _accommodations.SelectMany(a => a.Bookings)
                               .Concat(_activities.SelectMany(a => a.Bookings));
            return allBookings.Any()
                ? allBookings.Average(booking => booking.Cost)
                : 0;
        }
    }
}
