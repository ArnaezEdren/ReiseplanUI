using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Reiseanwendung.Application.Model
{
    [Table("Transportation")]
    public class Transportation
    {
        public Guid Id { get; set; }
        public string? Type { get; set; }
        public string BookingNumber { get; set; } // Added BookingNumber
        public bool IsRoundTrip { get; set; } // Indicates if it's a round-trip or one-way
        public decimal Cost { get; set; } // Added Cost

        public Transportation()
        {
            Id = Guid.NewGuid();
            BookingNumber = Guid.NewGuid().ToString();
        }

        public Transportation(string type, string bookingNumber, bool isRoundTrip, decimal cost)
        {
            Id = Guid.NewGuid();
            Type = type;
            BookingNumber = bookingNumber;
            IsRoundTrip = isRoundTrip;
            Cost = cost;
        }
    }

    public class Flight : Transportation
    {
        public Flight(string bookingNumber, bool isRoundTrip, decimal cost)
            : base("Flight", bookingNumber, isRoundTrip, cost) { }
    }

    public class Bus : Transportation
    {
        public Bus(string bookingNumber, bool isRoundTrip, decimal cost)
            : base("Bus", bookingNumber, isRoundTrip, cost) { }
    }

    public class Train : Transportation
    {
        public Train(string bookingNumber, bool isRoundTrip, decimal cost)
            : base("Train", bookingNumber, isRoundTrip, cost) { }
    }
}
