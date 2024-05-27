using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reiseanwendung.Application.Model
{
   
    public class Traveler : Person
    {
        public string? PassportNumber { get; set; }


        public Traveler() : base()
        {
        }


        public Traveler(string name, string passportNumber) : base(name)
        {
            PassportNumber = passportNumber;
        }

    }

}
