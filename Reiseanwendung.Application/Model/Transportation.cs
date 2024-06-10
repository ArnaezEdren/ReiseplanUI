using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Reiseanwendung.Application.Model
{
    [Table("Transportation")]
    public class Transportation : IEntity<int>
    {
        public Guid Guid { get; private set; }
        public int Id { get; set; }
        public string? Type { get; set; }
        public string BookingNumber { get; set; }
        public bool IsRoundTrip { get; set; }
        public decimal Cost { get; set; }

        public Transportation()
        {
            Guid = Guid.NewGuid();
            BookingNumber = Guid.NewGuid().ToString();
        }

        public Transportation(string type, string bookingNumber, bool isRoundTrip, decimal cost) : this()
        {
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
