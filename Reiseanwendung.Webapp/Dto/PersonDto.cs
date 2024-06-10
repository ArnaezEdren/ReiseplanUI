using System.ComponentModel.DataAnnotations;

namespace Reiseanwendung.Webapp.Dto
{
    public record PersonDto
    {
  
  
            public Guid Guid { get; set; }
            public int Id { get; set; }

            [Required(ErrorMessage = "Person name is required.")]
            public string? Name { get; set; }
            public string? Role { get; set; }
        
        
    }
}
