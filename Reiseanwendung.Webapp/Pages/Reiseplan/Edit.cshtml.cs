using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Reiseanwendung.Application.Infrastructure;
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

        [BindProperty]
        public DestinationDto Destination { get; set; } = null!;

        public async Task<IActionResult> OnGetAsync(Guid travelPlanId, Guid destinationId)
        {
            var travelPlan = await _db.TravelPlans
                .Include(tp => tp.Destinations)
                    .ThenInclude(d => d.Activities)
                .Include(tp => tp.Destinations)
                    .ThenInclude(d => d.Accommodations)
                        .ThenInclude(a => a.Address)
                .Include(tp => tp.Destinations)
                    .ThenInclude(d => d.Transportations)
                .FirstOrDefaultAsync(tp => tp.Guid == travelPlanId);

            if (travelPlan == null)
            {
                return RedirectToPage("/Reiseplan/Index");
            }

            var destination = travelPlan.Destinations.FirstOrDefault(d => d.Guid == destinationId);
            if (destination == null)
            {
                return RedirectToPage("/Reiseplan/Index");
            }

            TravelPlan = _mapper.Map<TravelplanDto>(travelPlan);
            Destination = _mapper.Map<DestinationDto>(destination);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid travelPlanId, Guid destinationId)
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
                        .ThenInclude(a => a.Address)
                .Include(tp => tp.Destinations)
                    .ThenInclude(d => d.Transportations)
                .FirstOrDefaultAsync(tp => tp.Guid == travelPlanId);

            if (travelPlan == null)
            {
                return RedirectToPage("/Reiseplan/Index");
            }

            var destination = travelPlan.Destinations.FirstOrDefault(d => d.Guid == destinationId);
            if (destination == null)
            {
                return RedirectToPage("/Reiseplan/Index");
            }

            // Manually map the updated properties to the entity
            destination.City = Destination.City;
            destination.Country = Destination.Country;

            // Update Activities
            foreach (var activityDto in Destination.Activities)
            {
                var activity = destination.Activities.FirstOrDefault(a => a.Guid == activityDto.Guid);
                if (activity != null)
                {
                    activity.Name = activityDto.Name;
                    activity.Description = activityDto.Description;
                    activity.StartDateTime = activityDto.StartDateTime;
                    activity.EndDateTime = activityDto.EndDateTime;
                }
            }

            // Update Accommodations
            foreach (var accommodationDto in Destination.Accommodations)
            {
                var accommodation = destination.Accommodations.FirstOrDefault(a => a.Guid == accommodationDto.Guid);
                if (accommodation != null)
                {
                    accommodation.Name = accommodationDto.Name;
                    accommodation.Address.Street = accommodationDto.Address.Street;
                    accommodation.Address.City = accommodationDto.Address.City;
                    accommodation.Address.Country = accommodationDto.Address.Country;
                    accommodation.Address.ZipCode = accommodationDto.Address.ZipCode;
                }
            }

            // Update Transportations
            foreach (var transportationDto in Destination.Transportations)
            {
                var transportation = destination.Transportations.FirstOrDefault(t => t.Guid == transportationDto.Guid);
                if (transportation != null)
                {
                    transportation.Type = transportationDto.Type;
                    transportation.BookingNumber = transportationDto.BookingNumber;
                    transportation.Cost = transportationDto.Cost;
                    transportation.IsRoundTrip = transportationDto.IsRoundTrip;
                }
            }

            _db.Entry(travelPlan).State = EntityState.Modified;
            _db.Entry(destination).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                ModelState.AddModelError("", $"Fehler beim Schreiben in die Datenbank: {ex.Message}");
                return Page();
            }

            return RedirectToPage("/Reiseplan/Details", new { guid = travelPlanId });
        }
    }
}
