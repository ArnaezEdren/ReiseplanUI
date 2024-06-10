using Reiseanwendung.Webapp.Dto.Reiseanwendung.Webapp.TravelplanDto;
using System.ComponentModel.DataAnnotations;

namespace Reiseanwendung.Webapp.Dto
{
    public record AccommodationDto
    {
        public Guid Guid { get; set; }
        public int Id { get; set; }

        [Required(ErrorMessage = "Accommodation name is required.")]
        public string? Name { get; set; }

        public AddressDto Address { get; set; } = new AddressDto();
    }
}
