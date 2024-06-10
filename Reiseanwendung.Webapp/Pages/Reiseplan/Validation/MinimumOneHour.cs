using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reiseanwendung.Webapp.Dto;
using Reiseanwendung.Webapp.Dto.Reiseanwendung.Webapp.TravelplanDto;

namespace Reiseanwendung.Webapp.Pages.Reiseplan.Validation
{
    public class MinimumOneHourDurationAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is null)
            {
                return ValidationResult.Success; // Treat null as valid for this custom attribute
            }

            var activities = value as List<ActivityDto>;
            if (activities != null)
            {
                foreach (var activity in activities)
                {
                    if ((activity.EndDateTime - activity.StartDateTime).TotalHours < 1)
                    {
                        return new ValidationResult("The activity must be at least one hour long.");
                    }
                }
            }
            return ValidationResult.Success;
        }
    }
}
