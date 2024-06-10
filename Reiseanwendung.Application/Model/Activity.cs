using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Reiseanwendung.Application.Model
{
    [Table("Activity")]
    public class Activity : IEntity<int>
    {
        public Guid Guid { get; private set; }
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public int DurationInHours { get; set; }
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();

        public Activity()
        {
            Guid = Guid.NewGuid();
        }

        public Activity(string name, string description, DateTime startDateTime, DateTime endDateTime) : this()
        {
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
