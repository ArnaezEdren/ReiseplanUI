using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Reiseanwendung.Application.Infrastructure;
using Reiseanwendung.Application.Model;
using Reiseanwendung.Webapp.Dto;
using Reiseanwendung.Webapp.Dto.Reiseanwendung.Webapp.TravelplanDto;
using System;
using System.Linq;

namespace Reiseanwendung.Webapp.Pages.Reiseplan
{
    public class EditModel : PageModel
    {
        private readonly ReiseplanContext _db;

        public EditModel(ReiseplanContext db)
        {
            _db = db;
        }

        [BindProperty]
        public TravelplanDto TravelPlan { get; set; } = null!;

        public IActionResult OnPost(Guid guid)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var travelPlan = _db.TravelPlans.Include(tp => tp.People).FirstOrDefault(t => t.Id == guid);

            if (travelPlan is null)
            {
                return RedirectToPage("/Reiseplan/Index");
            }

            travelPlan.Name = TravelPlan.Name;
            travelPlan.StartDate = TravelPlan.StartDate;
            travelPlan.EndDate = TravelPlan.EndDate;

            try
            {
                _db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Fehler beim Schreiben in die Datenbank!");
                return Page();
            }

            return RedirectToPage("/Reiseplan/Details", new { guid = travelPlan.Id });
        }

        public IActionResult OnGet(Guid guid)
        {
            var travelPlan = _db.TravelPlans.Include(tp => tp.People).FirstOrDefault(t => t.Id == guid);

            if (travelPlan is null)
            {
                return RedirectToPage("/Reiseplan/Index");
            }

            TravelPlan = new TravelplanDto(
                Id: travelPlan.Id,
                Name: travelPlan.Name,
                StartDate: travelPlan.StartDate,
                EndDate: travelPlan.EndDate
            );

            return Page();
        }
    }
}
