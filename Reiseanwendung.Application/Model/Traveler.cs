using System;

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
