using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Reiseanwendung.Application.Model
{
    [Table("Address")]
    public class Address : IEntity<int>
    {
        public Guid Guid { get; private set; }
        public int Id { get; set; }
        public string? Street { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public string? ZipCode { get; set; }

        public Address()
        {
            Guid = Guid.NewGuid();
        }

        public Address(string street, string city, string country, string zipCode) : this()
        {
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
