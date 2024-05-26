using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reiseanwendung.Application.Model
{
    [Table("Travelplan")]
    public class TravelPlan
    {


        public Guid Id { get; private set; }
        public string? Name { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public virtual ICollection<Person> People { get; set; }
        public virtual ICollection<Destination> Destinations { get; private set; }

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
            if (destination == null)
                throw new ArgumentNullException(nameof(destination));

            Destinations.Add(destination);
        }

        public bool IsGuideAvailable(Guid guideId, List<Person> people)
        {
            return people.Any(person => person.Id == guideId && person is Guide);
        }

        public decimal CalculateTotalCost()
        {

            return Destinations.Sum(dest => dest.Bookings.Sum(bk => bk.Cost));
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
