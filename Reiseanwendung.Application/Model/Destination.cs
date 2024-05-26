using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reiseanwendung.Application.Model
{
    [Table("Destination")]
    public class Destination
    {
        private readonly List<Activity> _activities = new List<Activity>();
        private readonly List<Booking> _bookings = new List<Booking>();

        public Guid Id { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }


        public IReadOnlyCollection<Activity> Activities => _activities.AsReadOnly();
        public IReadOnlyCollection<Booking> Bookings => _bookings.AsReadOnly();



        public void AddActivity(Activity activity)
        {
            if (activity == null)
                throw new ArgumentNullException(nameof(activity));
            _activities.Add(activity);
        }

        public void AddBooking(Booking booking)
        {
            if (booking == null)
                throw new ArgumentNullException(nameof(booking));
            _bookings.Add(booking);
        }


        public decimal CalculateAverageBookingCost()
        {
            return Bookings != null && Bookings.Any()
                ? Bookings.Average(booking => booking.Cost)
                : 0;
        }


    }

}
