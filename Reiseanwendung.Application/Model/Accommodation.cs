using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Reiseanwendung.Application.Model
{
    public class Accommodation : IEntity<int>
    {
        public Guid Guid { get; private set; }
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public Address Address { get; set; } = new Address();

        public List<Booking> Bookings { get; set; } = new List<Booking>();

        // Parameterless constructor
        public Accommodation()
        {
            Guid = Guid.NewGuid();
        }

        // Constructor with parameters
        public Accommodation(string name, Address address) : this()
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
