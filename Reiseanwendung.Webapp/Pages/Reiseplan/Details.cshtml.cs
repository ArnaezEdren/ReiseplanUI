using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Reiseanwendung.Application.Infrastructure;
using Reiseanwendung.Application.Model;
using System;
using System.Linq;

namespace Reiseanwendung.Webapp.Pages.Reiseplan
{
    public class DetailsModel : PageModel
    {
        private readonly ReiseplanContext _db;

        public DetailsModel(ReiseplanContext db)
        {
            _db = db;
        }

        public TravelPlan TravelPlan { get; private set; } = default!;
        public decimal TotalCost { get; private set; } = 0;

        public IActionResult OnGet(Guid guid)
        {
            var travelPlan = _db.TravelPlans
                .Include(tp => tp.Destinations)
                    .ThenInclude(d => d.Activities)
                        .ThenInclude(a => a.Bookings)
                .Include(tp => tp.Destinations)
                    .ThenInclude(d => d.Accommodations)
                        .ThenInclude(a => a.Bookings)
                .Include(tp => tp.Destinations)
                    .ThenInclude(d => d.Transportations)
                .Include(tp => tp.People)
                .FirstOrDefault(t => t.Id == guid);

            if (travelPlan == null)
            {
                return RedirectToPage("/Reiseplan/Index");
            }

            TravelPlan = travelPlan;

            // Calculate total cost
            TotalCost = travelPlan.Destinations.Sum(d => d.Accommodations.Sum(a => a.Bookings.Sum(b => b.Cost)) +
                                                         d.Activities.Sum(a => a.Bookings.Sum(b => b.Cost)) +
                                                         d.Transportations.Sum(t => t.Cost));

            return Page();
        }
    }
}
