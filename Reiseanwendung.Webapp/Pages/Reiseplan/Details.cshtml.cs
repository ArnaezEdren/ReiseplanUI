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

        public IActionResult OnGet(Guid guid)
        {
            TravelPlan = _db.TravelPlans
                .Include(tp => tp.Destinations)
                    .ThenInclude(d => d.Activities)
                .Include(tp => tp.Destinations)
                    .ThenInclude(d => d.Bookings)
                .Include(tp => tp.People)
                .FirstOrDefault(t => t.Id == guid);

            if (TravelPlan == null)
            {
                return RedirectToPage("/Reiseplan/Index");
            }

            return Page();
        }
    }
}
