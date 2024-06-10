using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Reiseanwendung.Application.Infrastructure;
using Reiseanwendung.Application.Model;
using Reiseanwendung.Webapp.Dto.Reiseanwendung.Webapp.TravelplanDto;
using System;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task<IActionResult> OnGetAsync(Guid guid)
        {
            var travelPlan = await _db.TravelPlans
                .Include(tp => tp.Destinations)
                .ThenInclude(d => d.Activities)
                .Include(tp => tp.Destinations)
                .ThenInclude(d => d.Accommodations)
                .Include(tp => tp.Destinations)
                .ThenInclude(d => d.Transportations)
                .FirstOrDefaultAsync(tp => tp.Guid == guid);

            if (travelPlan is null)
            {
                return RedirectToPage("/Reiseplan/Index");
            }

            TravelPlan = _mapper.Map<TravelplanDto>(travelPlan);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var travelPlan = await _db.TravelPlans
                .Include(tp => tp.Destinations)
                .ThenInclude(d => d.Activities)
                .Include(tp => tp.Destinations)
                .ThenInclude(d => d.Accommodations)
                .Include(tp => tp.Destinations)
                .ThenInclude(d => d.Transportations)
                .FirstOrDefaultAsync(tp => tp.Guid == TravelPlan.Guid);

            if (travelPlan is null)
            {
                return RedirectToPage("/Reiseplan/Index");
            }

            _mapper.Map(TravelPlan, travelPlan);

            _db.Entry(travelPlan).State = EntityState.Modified;
            try
            {
                await _db.SaveChangesAsync();
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
