using Microsoft.EntityFrameworkCore;
using Reiseanwendung.Application.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reiseanwendung.Application.Model
{

    public class TravelPlanService
    {
        public int Id { get; set; }
        private readonly ReiseplanContext _context;

        public TravelPlanService(ReiseplanContext context)
        {
            _context = context;
        }

        public bool IsGuideAvailable(Guid guideId, Guid travelPlanId)
        {

            var travelPlan = _context.TravelPlans.Include(tp => tp.People).FirstOrDefault(tp => tp.Id == travelPlanId);
            return travelPlan?.People.Any(person => person.Id == guideId && person is Guide) ?? false;
        }
    }

}
