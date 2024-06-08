using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Reiseanwendung.Application.Model
{
    [Table("Travelplan")]
    public class TravelPlan
    {
        public Guid Id { get; private set; }
        public string? Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ICollection<Person> People { get; set; } = new List<Person>();
        public ICollection<Destination> Destinations { get; set; } = new List<Destination>();

        public TravelPlan()
        {
            Id = Guid.NewGuid();
            People = new List<Person>();
            Destinations = new List<Destination>();
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

        public bool IsGuideAvailable(Guid guideId, List<Person> people)
        {
            return people.Any(person => person.Id == guideId && person is Guide);
        }

        public decimal CalculateTotalCost()
        {
            return Destinations.Sum(dest => dest.Accommodations.Sum(acc => acc.Bookings.Sum(bk => bk.Cost) ?? 0) +
                                                dest.Activities.Sum(act => act.Bookings.Sum(bk => bk.Cost) ?? 0));
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
