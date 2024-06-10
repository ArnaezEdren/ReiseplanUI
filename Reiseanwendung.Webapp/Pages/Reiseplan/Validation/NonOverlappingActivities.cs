using Reiseanwendung.Webapp.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Reiseanwendung.Webapp.Pages.Reiseplan.Validation
{
    public class NonOverlappingActivitiesAttribute : ValidationAttribute
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
                for (int i = 0; i < activities.Count; i++)
                {
                    for (int j = i + 1; j < activities.Count; j++)
                    {
                        if (activities[i].StartDateTime < activities[j].EndDateTime && activities[i].EndDateTime > activities[j].StartDateTime)
                        {
                            return new ValidationResult("Activities should not overlap.");
                        }
                    }
                }
            }
            return ValidationResult.Success;
        }
    }
}
