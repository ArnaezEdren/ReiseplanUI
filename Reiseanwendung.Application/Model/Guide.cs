using System;

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
