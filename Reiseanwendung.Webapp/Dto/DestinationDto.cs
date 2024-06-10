using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Reiseanwendung.Webapp.Dto
{
    public record DestinationDto
    {
        public Guid Guid { get; set; }
        public int Id { get; set; }

        [Required(ErrorMessage = "City is required.")]
        public string? City { get; set; }

        [Required(ErrorMessage = "Country is required.")]
        public string? Country { get; set; }

        public List<ActivityDto> Activities { get; set; } = new List<ActivityDto>();
        public List<AccommodationDto> Accommodations { get; set; } = new List<AccommodationDto>();
        public List<TransportationDto> Transportations { get; set; } = new List<TransportationDto>();
    }
}
