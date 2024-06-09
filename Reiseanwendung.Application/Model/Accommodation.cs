using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;

namespace Reiseanwendung.Application.Model
{
    public class Accommodation
    {
        public Guid Id { get; set; }

        [Required]
        public string? Name { get; set; }

        public Address Address { get; set; } = new Address();

        public List<Booking> Bookings { get; set; } = new List<Booking>();

        // Parameterless constructor
        public Accommodation() { }

        // Optional: Constructor with parameters if needed
        public Accommodation(string name, Address address)
        {
            Name = name;
            Address = address;
        }

        // Method to get the full address as a string
        public string GetFullAddress()
        {
            return $"{Address.Street}, {Address.City}, {Address.Country}, {Address.ZipCode}";
        }
    }
}
