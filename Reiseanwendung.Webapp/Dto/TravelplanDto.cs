using System;
using System.Collections.Generic;
using Reiseanwendung.Application.Model;

namespace Reiseanwendung.Webapp.Dto
{
    using System;

    using System.ComponentModel.DataAnnotations;
    using global::Reiseanwendung.Webapp.Pages.Reiseplan.Validation;


    namespace Reiseanwendung.Webapp.TravelplanDto
    {
        public record TravelplanDto(
            Guid Id,

            [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name length can't be more than 100.")]
        string? Name,

            [Required(ErrorMessage = "Start date is required")]
        [FutureDate(ErrorMessage = "Start date must be in the future and not the same as the current date")]
        DateTime StartDate,

            [Required(ErrorMessage = "End date is required")]
        [DateGreaterThan("StartDate", ErrorMessage = "End date must be greater than start date")]
        DateTime EndDate

             );
    }

}