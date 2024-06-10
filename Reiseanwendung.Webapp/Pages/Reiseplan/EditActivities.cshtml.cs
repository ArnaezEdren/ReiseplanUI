using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Reiseanwendung.Application.Infrastructure;
using Reiseanwendung.Application.Model;
using Reiseanwendung.Webapp.Dto;
using System;
using System.Threading.Tasks;

namespace Reiseanwendung.Webapp.Pages.Reiseplan
{
    public class EditActivitiesModel : PageModel
    {
        private readonly ReiseplanContext _db;
        private readonly IMapper _mapper;

        public EditActivitiesModel(ReiseplanContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        [BindProperty]
        public ActivityDto Activity { get; set; } = null!;

        [BindProperty]
        public Guid TravelPlanGuid { get; set; }

        public IActionResult OnGet(Guid guid)
        {
            var activity = _db.Activities
                .Include(a => a.Destination)
                .ThenInclude(d => d.TravelPlan) // Ensure we can get the TravelPlanId
                .FirstOrDefault(a => a.Guid == guid);

            if (activity is null)
            {
                return RedirectToPage("/Reiseplan/Details");
            }

            Activity = _mapper.Map<ActivityDto>(activity);
            TravelPlanGuid = activity.Destination.TravelPlan.Guid;

            return Page();
        }

        public IActionResult OnPost(Guid guid)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var existingActivity = _db.Activities
                .Include(a => a.Destination)
                .ThenInclude(d => d.TravelPlan) // Ensure we can get the TravelPlanId
                .FirstOrDefault(a => a.Guid == guid);

            if (existingActivity == null)
            {
                return RedirectToPage("/Reiseplan/Index");
            }

            // Manually update the properties that are allowed to change
            existingActivity.Name = Activity.Name;
            existingActivity.Description = Activity.Description;
            existingActivity.StartDateTime = Activity.StartDateTime;
            existingActivity.EndDateTime = Activity.EndDateTime;

            try
            {
                _db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Fehler beim Schreiben in die Datenbank");
                return Page();
            }

            return RedirectToPage("/Reiseplan/Details", new { guid = TravelPlanGuid });
        }
    }
}
