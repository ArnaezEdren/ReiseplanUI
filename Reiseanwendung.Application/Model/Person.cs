using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reiseanwendung.Application.Model
{
    [Table("Persons")]
    public class Person
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public ICollection<TravelPlan> TravelPlans { get; set; } = new List<TravelPlan>();

        public Person()
        {
            Id = Guid.NewGuid();
            TravelPlans = new List<TravelPlan>();
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
    }



}
