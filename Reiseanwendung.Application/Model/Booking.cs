using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Reiseanwendung.Application.Model
{
    [Table("Booking")]
    public class Booking : IEntity<int>
    {
        public Guid Guid { get; private set; }
        public int Id { get; set; }
        public decimal? Cost { get; set; }
        public DateTime Date { get; set; }
        public string? BookingNumber { get; set; }
        public int? ActivityId { get; set; }
        public int? AccommodationId { get; set; }
        public Activity? Activity { get; set; }
        public Accommodation? Accommodation { get; set; }

        public Booking()
        {
            Guid = Guid.NewGuid();
            Date = DateTime.Now;
        }

        public Booking(decimal cost, DateTime date, string bookingNumber) : this()
        {
            Cost = cost;
            Date = date;
            BookingNumber = bookingNumber;
        }
    }
}
