using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Reiseanwendung.Webapp.Pages.Reiseplan.Validation
{
    public class DifferentDayAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is null)
            {
                return ValidationResult.Success; // Treat null as valid for this custom attribute
            }

            var entityDates = value as List<DateTime>;
            if (entityDates != null)
            {
                if (entityDates.Distinct().Count() != entityDates.Count)
                {
                    return new ValidationResult("Dates should not be on the same day.");
                }
            }
            return ValidationResult.Success;
        }
    }
}
