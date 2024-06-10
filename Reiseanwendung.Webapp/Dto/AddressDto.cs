using System.ComponentModel.DataAnnotations;

namespace Reiseanwendung.Webapp.Dto
{
    public record AddressDto
    {
        public Guid Guid { get; set; }
        public int Id { get; set; }

        [Required(ErrorMessage = "Street is required.")]
        public string? Street { get; set; }

        [Required(ErrorMessage = "City is required.")]
        public string? City { get; set; }

        [Required(ErrorMessage = "Country is required.")]
        public string? Country { get; set; }

        [Required(ErrorMessage = "Zip code is required.")]
        public string? ZipCode { get; set; }
    }
}
