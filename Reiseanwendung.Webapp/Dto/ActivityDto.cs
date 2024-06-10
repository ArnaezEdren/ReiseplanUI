using System;
using System.ComponentModel.DataAnnotations;

namespace Reiseanwendung.Webapp.Dto
{ 
    public record ActivityDto 
    {
        public Guid Guid { get; set; }
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        public string? Description { get; set; }

        [Required]
        public DateTime StartDateTime { get; set; }

        [Required]
        public DateTime EndDateTime { get; set; }

        // Foreign key
        public Guid DestinationGuid { get; set; }

  
    }
}
