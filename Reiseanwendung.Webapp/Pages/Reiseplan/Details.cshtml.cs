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
                .FirstOrDefault(t => t.Guid == guid);

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

        public async Task<IActionResult> OnPostDeleteDestinationAsync(Guid travelPlanId, Guid destinationId)
        {
            var travelPlan = await _db.TravelPlans
                .Include(tp => tp.Destinations)
                .FirstOrDefaultAsync(tp => tp.Guid == travelPlanId);

            if (travelPlan == null)
            {
                return NotFound();
            }

            var destination = travelPlan.Destinations.FirstOrDefault(d => d.Guid == destinationId);
            if (destination != null)
            {
                travelPlan.Destinations.Remove(destination);
                await _db.SaveChangesAsync();
            }

            return RedirectToPage(new { guid = travelPlanId });
        }

        public async Task<IActionResult> OnPostDeleteActivityAsync(Guid travelPlanId, Guid destinationId, Guid activityId)
        {
            var travelPlan = await _db.TravelPlans
                .Include(tp => tp.Destinations)
                    .ThenInclude(d => d.Activities)
                .FirstOrDefaultAsync(tp => tp.Guid == travelPlanId);

            if (travelPlan == null)
            {
                return NotFound();
            }

            var destination = travelPlan.Destinations.FirstOrDefault(d => d.Guid == destinationId);
            if (destination != null)
            {
                var activity = destination.Activities.FirstOrDefault(a => a.Guid == activityId);
                if (activity != null)
                {
                    destination.Activities.Remove(activity);
                    await _db.SaveChangesAsync();
                }
            }

            return RedirectToPage(new { guid = travelPlanId });
        }

        public async Task<IActionResult> OnPostDeleteAccommodationAsync(Guid travelPlanId, Guid destinationId, Guid accommodationId)
        {
            var travelPlan = await _db.TravelPlans
                .Include(tp => tp.Destinations)
                    .ThenInclude(d => d.Accommodations)
                .FirstOrDefaultAsync(tp => tp.Guid == travelPlanId);

            if (travelPlan == null)
            {
                return NotFound();
            }

            var destination = travelPlan.Destinations.FirstOrDefault(d => d.Guid == destinationId);
            if (destination != null)
            {
                var accommodation = destination.Accommodations.FirstOrDefault(a => a.Guid == accommodationId);
                if (accommodation != null)
                {
                    destination.Accommodations.Remove(accommodation);
                    await _db.SaveChangesAsync();
                }
            }

            return RedirectToPage(new { guid = travelPlanId });
        }

        public async Task<IActionResult> OnPostDeleteTransportationAsync(Guid travelPlanId, Guid destinationId, Guid transportationId)
        {
            var travelPlan = await _db.TravelPlans
                .Include(tp => tp.Destinations)
                    .ThenInclude(d => d.Transportations)
                .FirstOrDefaultAsync(tp => tp.Guid == travelPlanId);

            if (travelPlan == null)
            {
                return NotFound();
            }

            var destination = travelPlan.Destinations.FirstOrDefault(d => d.Guid == destinationId);
            if (destination != null)
            {
                var transportation = destination.Transportations.FirstOrDefault(t => t.Guid == transportationId);
                if (transportation != null)
                {
                    destination.Transportations.Remove(transportation);
                    await _db.SaveChangesAsync();
                }
            }

            return RedirectToPage(new { guid = travelPlanId });
        }
    }
}