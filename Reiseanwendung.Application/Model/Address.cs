using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reiseanwendung.Application.Model
{
    [Table("Address")]

    public class Address
    {
        public Guid Id { get; set; }  // Add this line
        public string? Street { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public string? ZipCode { get; set; }


        private Address() { }

        public Address(string street, string city, string country, string zipCode)
        {
            Id = Guid.NewGuid();  // Initialize the Id
            Street = street;
            City = city;
            Country = country;
            ZipCode = zipCode;
        }


        protected bool Equals(Address other)
        {
            return Street == other.Street && City == other.City && Country == other.Country && ZipCode == other.ZipCode;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Address)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Street, City, Country, ZipCode);
        }


    }

}
