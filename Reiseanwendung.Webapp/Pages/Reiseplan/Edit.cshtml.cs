using AutoMapper;
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
        private readonly IMapper _mapper;

        public EditModel(ReiseplanContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        [BindProperty]
        public TravelplanDto TravelPlan { get; set; } = null!;

        public IActionResult OnGet(Guid guid)
        {
            var travelPlan = _db.TravelPlans.FirstOrDefault(t => t.Id == guid);
            if (travelPlan is null)
            {
                return RedirectToPage("/Reiseplan/Index");
            }
            TravelPlan = _mapper.Map<TravelplanDto>(travelPlan);
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var travelPlan = _db.TravelPlans.FirstOrDefault(t => t.Id == TravelPlan.Id);
            if (travelPlan is null)
            {
                return RedirectToPage("/Reiseplan/Index");
            }

            // Map updated values from DTO to the existing entity
            _mapper.Map(TravelPlan, travelPlan);

            _db.Entry(travelPlan).State = EntityState.Modified;
            try
            {
                _db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Fehler beim Schreiben in die Datenbank!");
                return Page();
            }

            return RedirectToPage("/Reiseplan/Index");
        }
    }
}
