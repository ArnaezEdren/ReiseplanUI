using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Reiseanwendung.Application.Infrastructure;
using Reiseanwendung.Application.Model;
using Reiseanwendung.Webapp.Dto;
using System;
using System.Linq;

namespace Reiseanwendung.Webapp.Pages.Reiseplan
{
    public class EditAccommodationsModel : PageModel
    {
        private readonly ReiseplanContext _db;
        private readonly IMapper _mapper;

        public EditAccommodationsModel(ReiseplanContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        [BindProperty]
        public AccommodationDto Accommodation { get; set; } = null!;

        [BindProperty]
        public Guid TravelPlanGuid { get; set; }

        public IActionResult OnGet(Guid guid)
        {
            var accommodation = _db.Accommodations.FirstOrDefault(a => a.Guid == guid);

            if (accommodation == null)
            {
                return RedirectToPage("/Reiseplan/Index");
            }

            Accommodation = _mapper.Map<AccommodationDto>(accommodation);

            // Ensure Accommodation.Address is not null
            if (Accommodation.Address == null)
            {
                Accommodation.Address = new AddressDto();
            }

            // Ensure the TravelPlanGuid is properly set
            if (accommodation.Destination?.TravelPlan != null)
            {
                TravelPlanGuid = accommodation.Destination.TravelPlan.Guid;
            }
            else
            {
                // Handle case where TravelPlan is not properly set
                return RedirectToPage("/Reiseplan/Index");
            }

            return Page();
        }

        public IActionResult OnPost(Guid guid)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var accommodation = _db.Accommodations
                .Include(a => a.Address) // Ensure the Address is included
                .Include(a => a.Destination)
                .ThenInclude(d => d.TravelPlan)
                .FirstOrDefault(a => a.Guid == guid);

            if (accommodation == null)
            {
                return RedirectToPage("/Reiseplan/Index");
            }

            // Manually update the properties that are allowed to change
            accommodation.Name = Accommodation.Name;
            if (accommodation.Address != null)
            {
                accommodation.Address.Street = Accommodation.Address.Street;
                accommodation.Address.City = Accommodation.Address.City;
                accommodation.Address.Country = Accommodation.Address.Country;
                accommodation.Address.ZipCode = Accommodation.Address.ZipCode;
            }
            else
            {
                // If Address is null, create a new Address object
                accommodation.Address = new Address
                {
                    Street = Accommodation.Address.Street,
                    City = Accommodation.Address.City,
                    Country = Accommodation.Address.Country,
                    ZipCode = Accommodation.Address.ZipCode
                };
            }

            try
            {
                _db.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                ModelState.AddModelError("", "Fehler beim Schreiben in die Datenbank: " + ex.Message);
                return Page();
            }

            return RedirectToPage("/Reiseplan/Details", new { travelPlanId = TravelPlanGuid });
        }
    }
}
