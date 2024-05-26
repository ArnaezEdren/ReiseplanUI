using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reiseanwendung.Application.Model
{
    [Table("Activity")]
    public class Activity
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public int DurationInHours { get; set; }

        public Activity() { }


        public Activity(string name, string description, DateTime startDateTime, DateTime endDateTime)
        {
            Id = Guid.NewGuid();
            Name = name;
            Description = description;
            StartDateTime = startDateTime;
            EndDateTime = endDateTime;
            DurationInHours = (int)(endDateTime - startDateTime).TotalHours;

            if (DurationInHours <= 0)
            {
                throw new ArgumentException("Die Endzeit muss nach der Startzeit liegen.");
            }
        }

    }


}
