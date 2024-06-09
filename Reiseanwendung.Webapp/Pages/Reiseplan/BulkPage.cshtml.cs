using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Reiseanwendung.Application.Infrastructure;
using Reiseanwendung.Application.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reiseanwendung.Webapp.Pages.Reiseplan
{
    public class BulkPageModel : PageModel
    {
        private readonly ReiseplanContext _context;

        public BulkPageModel(ReiseplanContext context)
        {
            _context = context;
        }

        [BindProperty]
        public List<TravelPlan> TravelPlans { get; set; } = new List<TravelPlan>();

        public async Task OnGetAsync()
        {
            TravelPlans = await _context.TravelPlans.ToListAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var existingPlans = await _context.TravelPlans.ToListAsync();

            // Remove TravelPlans that are not in the submitted list
            foreach (var plan in existingPlans)
            {
                if (!TravelPlans.Any(tp => tp.Id == plan.Id))
                {
                    _context.TravelPlans.Remove(plan);
                }
            }

            // Update TravelPlans in the submitted list
            foreach (var plan in TravelPlans)
            {
                var existingPlan = await _context.TravelPlans.FindAsync(plan.Id);
                if (existingPlan != null)
                {
                    existingPlan.Name = plan.Name;
                    existingPlan.StartDate = plan.StartDate;
                    existingPlan.EndDate = plan.EndDate;
                    _context.TravelPlans.Update(existingPlan);
                }
            }

            // Save all changes to the database
            await _context.SaveChangesAsync();

            return RedirectToPage("/Reiseplan/Index");
        }
    }
}
