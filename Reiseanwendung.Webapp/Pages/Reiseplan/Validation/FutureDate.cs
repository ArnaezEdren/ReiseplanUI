using System;
using System.ComponentModel.DataAnnotations;

namespace Reiseanwendung.Webapp.Pages.Reiseplan.Validation
{
    public class FutureDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value is null)
            {
                return true; // Treat null as valid for this custom attribute
            }

            if (value is DateTime dateTime)
            {
                return dateTime.Date > DateTime.Now.Date;
            }
            return false;
        }
    }
}
