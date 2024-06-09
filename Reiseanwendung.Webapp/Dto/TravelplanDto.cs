using System;
using System.Collections.Generic;

namespace Reiseanwendung.Webapp.Dto.Reiseanwendung.Webapp.TravelplanDto
{
    public class TravelplanDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;  // Initialize to avoid null reference
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<DestinationDto> Destinations { get; set; } = new List<DestinationDto>();  // Initialize to avoid null reference
        public List<PersonDto> People { get; set; } = new List<PersonDto>();  // Initialize to avoid null reference
    }

    public class DestinationDto
    {
        public Guid Id { get; set; }
        public string City { get; set; } = string.Empty;  // Initialize to avoid null reference
        public string Country { get; set; } = string.Empty;  // Initialize to avoid null reference
        public List<ActivityDto> Activities { get; set; } = new List<ActivityDto>();  // Initialize to avoid null reference
        public List<AccommodationDto> Accommodations { get; set; } = new List<AccommodationDto>();  // Initialize to avoid null reference
        public List<TransportationDto> Transportations { get; set; } = new List<TransportationDto>();  // Initialize to avoid null reference
    }

    public class ActivityDto
    {
        public string Name { get; set; } = string.Empty;  // Initialize to avoid null reference
        public string Description { get; set; } = string.Empty;  // Initialize to avoid null reference
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
    }

    public class AccommodationDto
    {
        public string Name { get; set; } = string.Empty;  // Initialize to avoid null reference
        public AddressDto Address { get; set; } = new AddressDto();  // Initialize to avoid null reference
    }

    public class TransportationDto
    {
        public string Type { get; set; } = string.Empty;  // Initialize to avoid null reference
        public string BookingNumber { get; set; } = string.Empty;  // Initialize to avoid null reference
        public bool IsRoundTrip { get; set; }
        public decimal Cost { get; set; }
    }

    public class AddressDto
    {
        public string Street { get; set; } = string.Empty;  // Initialize to avoid null reference
        public string City { get; set; } = string.Empty;  // Initialize to avoid null reference
        public string Country { get; set; } = string.Empty;  // Initialize to avoid null reference
        public string ZipCode { get; set; } = string.Empty;  // Initialize to avoid null reference
    }
}
