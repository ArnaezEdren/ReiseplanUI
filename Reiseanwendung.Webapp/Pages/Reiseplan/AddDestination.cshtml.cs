using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Reiseanwendung.Application.Infrastructure;
using Reiseanwendung.Application.Model;
using System;
using System.Threading.Tasks;

namespace Reiseanwendung.Webapp.Pages.Reiseplan
{
    public class AddDestinationModel : PageModel
    {
        private readonly ReiseplanContext _db;

        public AddDestinationModel(ReiseplanContext db)
        {
            _db = db;
        }

        [BindProperty]
        public Destination NewDestination { get; set; } = new Destination();

        public Guid TravelPlanId { get; set; }

        public void OnGet(Guid travelPlanId)
        {
            TravelPlanId = travelPlanId;
        }

        public async Task<IActionResult> OnPostAsync(Guid travelPlanId)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var travelPlan = await _db.TravelPlans
                  .Include(tp => tp.Destinations)
                  .FirstOrDefaultAsync(tp => tp.Guid == travelPlanId);
            if (travelPlan == null)
            {
                return NotFound();
            }

            travelPlan.Destinations.Add(NewDestination);
            await _db.SaveChangesAsync();

            return RedirectToPage("/Reiseplan/Details", new { guid = travelPlanId });
        }
    }
}