using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Reiseanwendung.Application.Model
{
    [Table("Booking")]
    public class Booking
    {
        public Guid Id { get; set; }
        public decimal? Cost { get; set; }
        public DateTime Date { get; set; }
        public string? BookingNumber { get; set; }
        public Guid? ActivityId { get; set; }
        public Guid? AccommodationId { get; set; }

        public Booking()
        {
            Id = Guid.NewGuid();
            Date = DateTime.Now;
        }

        public Booking(decimal cost, DateTime date, string bookingNumber)
        {
            Id = Guid.NewGuid();
            Cost = cost;
            Date = date;
            BookingNumber = bookingNumber;
        }
    }
}
