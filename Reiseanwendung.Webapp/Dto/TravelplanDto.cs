using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reiseanwendung.Webapp.Pages.Reiseplan.Validation;

namespace Reiseanwendung.Webapp.Dto.Reiseanwendung.Webapp.TravelplanDto
{
    public record TravelplanDto
    {
        public Guid Guid { get; set; } = Guid.NewGuid();
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Start date is required.")]
        [FutureDate(ErrorMessage = "Start date must be in the future.")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End date is required.")]
        [DateGreaterThan("StartDate", ErrorMessage = "End date must be greater than start date.")]
        public DateTime EndDate { get; set; }

        public List<PersonDto> People { get; set; } = new List<PersonDto>();
        public List<DestinationDto> Destinations { get; set; } = new List<DestinationDto>();

    }
}
