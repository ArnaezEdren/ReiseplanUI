using Bogus;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reiseanwendung.Application.Model
{
 
    public class Guide : Person
    {
        public string? LicenseNumber { get; set; }


        public Guide() : base()
        {
        }


        public Guide(string name, string licenseNumber) : base(name)
        {
            LicenseNumber = licenseNumber;
        }

    }

}
