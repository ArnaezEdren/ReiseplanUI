using Bogus.DataSets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reiseanwendung.Application.Model
{

    public class Accommodation
    {


        public Guid Id { get; set; }
        public string? Name { get; set; }
        public Address? Address { get; private set; }

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
