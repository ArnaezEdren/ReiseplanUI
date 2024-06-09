using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Reiseanwendung.Application.Infrastructure;
using Reiseanwendung.Application.Model;
using System.Collections.Generic;
using System.Linq;

namespace Reiseanwendung.Webapp.Pages.Reiseplan
{
    public class IndexModel : PageModel
    {
        private readonly ReiseplanContext _db;

        public IndexModel(ReiseplanContext db)
        {
            _db = db;
        }

        public List<TravelPlan> Travelplans { get; set; }

        public void OnGet()
        {
            Travelplans = _db.TravelPlans
                .Include(tp => tp.Destinations)
                    .ThenInclude(d => d.Activities)
                .Include(tp => tp.Destinations)
                    .ThenInclude(d => d.Accommodations)
                .Include(tp => tp.Destinations)
                    .ThenInclude(d => d.Transportations)
                .ToList();
        }
    }
}
