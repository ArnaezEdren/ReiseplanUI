using System.ComponentModel.DataAnnotations;
using Xunit.Sdk;

namespace Reiseanwendung.Webapp.Dto
{
    public record TransportationDto
    {
        
        public Guid Guid { get; set; }
        public int Id { get; set; }

        [Required(ErrorMessage = "Transportation type is required.")]
        public string? Type { get; set; }

        public string? BookingNumber { get; set; }
        public bool IsRoundTrip { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Cost must be a positive number.")]
        public decimal Cost { get; set; }
    }
}
