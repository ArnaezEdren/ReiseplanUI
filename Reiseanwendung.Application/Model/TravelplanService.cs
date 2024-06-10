using Microsoft.EntityFrameworkCore;
using Reiseanwendung.Application.Infrastructure;
using System;
using System.Linq;

namespace Reiseanwendung.Application.Model
{
    public class TravelPlanService
    {
        public Guid Guid { get; private set; }
        public int Id { get; set; }
   
        private readonly ReiseplanContext _context;

        public TravelPlanService(ReiseplanContext context)
        {
            _context = context;
        }

        public bool IsGuideAvailable(Guid guideId, Guid travelPlanId)
        {
            var travelPlan = _context.TravelPlans.Include(tp => tp.People).FirstOrDefault(tp => tp.Guid == travelPlanId);

            // Sicherstellen, dass travelPlan und People nicht null sind
            return travelPlan?.People != null && travelPlan.People.Any(person => person.Guid == guideId && person is Guide);
        }
    }
}
