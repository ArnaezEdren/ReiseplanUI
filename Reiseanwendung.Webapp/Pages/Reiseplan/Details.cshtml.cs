using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Reiseanwendung.Application.Infrastructure;
using Reiseanwendung.Application.Model;
using System;
using System.Linq;
using System.Threading.Tasks;

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
        public decimal? TotalCost { get; private set; }

        [BindProperty]
        public Person NewPerson { get; set; } = new Person();

        [BindProperty]
        public Destination NewDestination { get; set; } = new Destination();

        public async Task<IActionResult> OnGetAsync(Guid guid)
        {
            var travelPlan = await _db.TravelPlans
                .Include(tp => tp.Destinations)
                    .ThenInclude(d => d.Activities)
                        .ThenInclude(a => a.Bookings)
                .Include(tp => tp.Destinations)
                    .ThenInclude(d => d.Accommodations)
                        .ThenInclude(a => a.Bookings)
                .Include(tp => tp.Destinations)
                    .ThenInclude(d => d.Transportations)
                .Include(tp => tp.People)
                .FirstOrDefaultAsync(t => t.Id == guid);

            if (travelPlan == null)
            {
                return RedirectToPage("/Reiseplan/Index");
            }

            TravelPlan = travelPlan;

            // Calculate total cost
            TotalCost = travelPlan.Destinations.Sum(d =>
                d.Accommodations.Sum(a => a.Bookings.Sum(b => (decimal?)b.Cost) ?? 0) +
                d.Activities.Sum(a => a.Bookings.Sum(b => (decimal?)b.Cost) ?? 0) +
                d.Transportations.Sum(t => (decimal?)t.Cost ?? 0)
            );

            return Page();
        }

        public async Task<IActionResult> OnPostAddPersonAsync(Guid guid)
        {
            var travelPlan = await _db.TravelPlans.Include(tp => tp.People).FirstOrDefaultAsync(t => t.Id == guid);

            if (travelPlan == null)
            {
                return RedirectToPage("/Reiseplan/Index");
            }

            if (!ModelState.IsValid)
            {
                return RedirectToPage(new { guid });
            }

            travelPlan.People.Add(NewPerson);
            await _db.SaveChangesAsync();

            return RedirectToPage(new { guid });
        }

        public async Task<IActionResult> OnPostAddDestinationAsync(Guid guid)
        {
            var travelPlan = await _db.TravelPlans.Include(tp => tp.Destinations).FirstOrDefaultAsync(t => t.Id == guid);

            if (travelPlan == null)
            {
                return RedirectToPage("/Reiseplan/Index");
            }

            if (!ModelState.IsValid)
            {
                return RedirectToPage(new { guid });
            }

            travelPlan.Destinations.Add(NewDestination);
            await _db.SaveChangesAsync();

            return RedirectToPage(new { guid });
        }
    }
}
