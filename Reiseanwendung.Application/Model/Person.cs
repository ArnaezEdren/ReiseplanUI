using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Reiseanwendung.Application.Model
{
    [Table("Persons")]
    public class Person : IEntity<int>
    {
        public int Id { get; set; }
        public Guid Guid { get; private set; }
        public string? Name { get; set; }
        public virtual ICollection<TravelPlan> TravelPlans { get; set; } = new List<TravelPlan>();

        public Person()
        {
            Guid = Guid.NewGuid();
        }

        public Person(string name) : this()
        {
            Name = name;
        }

        public void AddTravelPlan(TravelPlan travelPlan)
        {
            if (travelPlan == null)
                throw new ArgumentNullException(nameof(travelPlan));

            TravelPlans.Add(travelPlan);
        }

        public bool IsAvailableOn(DateTime date)
        {
            return !TravelPlans.Any(tp => tp.StartDate <= date && tp.EndDate >= date);
        }

        public static bool CanDeletePerson(ICollection<Person> people)
        {
            return people.Count > 1;
        }
    }
}
