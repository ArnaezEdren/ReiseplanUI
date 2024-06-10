using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Reiseanwendung.Application.Model
{
    [Table("Travelplan")]
    public class TravelPlan : IEntity<int>
    {
        public Guid Guid { get; private set; }
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ICollection<Person>? People { get; set; } = new List<Person>();
        public ICollection<Destination> Destinations { get; set; } = new List<Destination>();

        public TravelPlan()
        {
            Guid = Guid.NewGuid();
        }

        public TravelPlan(string name, DateTime startDate, DateTime endDate) : this()
        {
            Name = name;
            StartDate = startDate;
            EndDate = endDate;
        }

        public void AddDestination(Destination destination)
        {
            destination.TravelPlan = this;
            destination.TravelPlanId = this.Id;
            Destinations.Add(destination);
        }

   
        

        public int GetTotalNumberOfActivities()
        {
            return Destinations.Sum(dest => dest.Activities.Count);
        }

        public Activity GetNextActivity()
        {
            var currentDate = DateTime.Now;
            var activity = Destinations.SelectMany(dest => dest.Activities)
                                           .Where(activity => activity.StartDateTime > currentDate)
                                           .OrderBy(activity => activity.StartDateTime)
                                           .FirstOrDefault();

            if (activity == null)
            {
                throw new InvalidOperationException("Keine zukünftigen Aktivitäten gefunden.");
            }

            return activity;
        }

        public IEnumerable<Destination> GetDestinationsByCountry(string country)
        {
            return Destinations.Where(destination => destination.Country == country);
        }
    }
}
