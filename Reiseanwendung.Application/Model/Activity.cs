using System;
using System.ComponentModel.DataAnnotations;

namespace Reiseanwendung.Application.Model
{
    public class Activity : IEntity<int>
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

        // Navigation property
        public Destination? Destination { get; set; }


        public Activity() {
            Guid = Guid.NewGuid();
        }
       

        public Activity(string name, string description, DateTime startDateTime, DateTime endDateTime)
        {
     
            Name=name;
            Description=description;
            StartDateTime=startDateTime;
            EndDateTime=endDateTime;

        }
    }

 
}
