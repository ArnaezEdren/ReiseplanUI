using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Reiseanwendung.Application.Infrastructure;
using Reiseanwendung.Application.Model;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Reiseanwendung.Webapp.Pages.Reiseplan
{
    public class EditDestinationModel : PageModel
    {
        private readonly ReiseplanContext _db;

        public EditDestinationModel(ReiseplanContext db)
        {
            _db = db;
        }

        [BindProperty]
        public Destination Destination { get; set; } = new Destination
        {
            Activities = new List<Activity>(),
            Accommodations = new List<Accommodation>(),
            Transportations = new List<Transportation>()
        };

        public Guid TravelPlanId { get; set; }
        public Guid DestinationId { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid travelPlanId, Guid destinationId)
        {
            TravelPlanId = travelPlanId;
            DestinationId = destinationId;

            var travelPlan = await _db.TravelPlans.FindAsync(travelPlanId);
            if (travelPlan == null)
            {
                return NotFound();
            }

            Destination = travelPlan.Destinations.FirstOrDefault(d => d.Id == destinationId) ?? new Destination();
            if (Destination == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid travelPlanId, Guid destinationId)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var travelPlan = await _db.TravelPlans.FindAsync(travelPlanId);
            if (travelPlan == null)
            {
                return NotFound();
            }

            var destinationToUpdate = travelPlan.Destinations.FirstOrDefault(d => d.Id == destinationId);
            if (destinationToUpdate == null)
            {
                return NotFound();
            }

            destinationToUpdate.City = Destination.City;
            destinationToUpdate.Country = Destination.Country;
            destinationToUpdate.Activities = Destination.Activities;
            destinationToUpdate.Accommodations = Destination.Accommodations;
            destinationToUpdate.Transportations = Destination.Transportations;

            await _db.SaveChangesAsync();

            return RedirectToPage("/Reiseplan/Details", new { guid = travelPlanId });
        }
    }
}
