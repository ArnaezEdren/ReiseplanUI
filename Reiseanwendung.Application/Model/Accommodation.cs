using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Reiseanwendung.Application.Model
{
    [Table("Accommodation")]
    public class Accommodation
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public Address? Address { get; set; }
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();

        public Accommodation() { }

        public Accommodation(string name, Address address)
        {
            Id = Guid.NewGuid();
            Name = name;
            Address = address;
        }

        public string GetFullAddress()
        {
            return Address != null ? $"{Address.Street}, {Address.City}, {Address.Country}, {Address.ZipCode}" : "Address not available";
        }
    }
}
