using System;
using System.Collections.Generic;

namespace Reiseanwendung.Webapp.Dto.Reiseanwendung.Webapp.TravelplanDto
{
    public class TravelplanDto
    {
       
        public Guid Guid { get; set; } = Guid.NewGuid();
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<PersonDto> People { get; set; } = new List<PersonDto>(); // Use List<PersonDto>
        public List<DestinationDto> Destinations { get; set; } = new List<DestinationDto>(); // Use List<DestinationDto>
    }

    public class DestinationDto
    {
        public Guid Guid   { get; set; }
        public int Id { get; set; }
        public string? City { get; set; } = string.Empty;
        public string? Country { get; set; } = string.Empty;
        public List<ActivityDto> Activities { get; set; } = new List<ActivityDto>();
        public List<AccommodationDto> Accommodations { get; set; } = new List<AccommodationDto>();
        public List<TransportationDto> Transportations { get; set; } = new List<TransportationDto>();
    }

    public class ActivityDto
    {
        public Guid Guid { get; set; }
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
    }

    public class AccommodationDto
    {
        public Guid Guid { get; set; }
        public int Id { get; set; }
        public string? Name { get; set; }
        public AddressDto Address { get; set; } = new AddressDto();
    }

    public class TransportationDto
    {
        public Guid Guid { get; set; }
        public int Id { get; set; }
        public string? Type { get; set; }
        public string? BookingNumber { get; set; }
        public bool IsRoundTrip { get; set; }
        public decimal Cost { get; set; }
    }

    public class AddressDto
    {
        public Guid Guid { get; set; }
        public int Id { get; set; }
        public string? Street { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public string? ZipCode { get; set; }
    }



}
