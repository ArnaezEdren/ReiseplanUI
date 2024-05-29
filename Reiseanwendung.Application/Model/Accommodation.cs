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
        public Address? Address { get; private set; }
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();

        public Accommodation() { }

        public Accommodation(string name, Address address)
        {
            Name = name;
            Address = address;
        }

        public string GetFullAddress()
        {
            if (Address == null)
            {
                return "Address not available";
            }
            return $"{Address.Street}, {Address.City}, {Address.Country}, {Address.ZipCode}";
        }
    }
}
