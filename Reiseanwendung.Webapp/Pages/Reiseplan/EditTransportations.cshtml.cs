using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Reiseanwendung.Application.Infrastructure;
using Reiseanwendung.Application.Model;
using Reiseanwendung.Webapp.Dto;
using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Threading.Tasks;
using Xunit.Sdk;

namespace Reiseanwendung.Webapp.Pages.Reiseplan
{
    public class EditTransportationsModel : PageModel
    {
        private readonly ReiseplanContext _db;
        private readonly IMapper _mapper;

        public EditTransportationsModel(ReiseplanContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        [BindProperty]
        public TransportationDto Transportation { get; set; } = null;


    
        public IActionResult OnGet(Guid guid)
        {
            var transportation = _db.Transportations.FirstOrDefault(t => t.Guid == guid);   
            if (transportation is null)
                {
                return RedirectToPage("/Reiseplan/Index");
            }
            Transportation =_mapper.Map<TransportationDto>(transportation);
            return Page();
        }

        public IActionResult OnPost(Guid guid)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var transportation = _db.Transportations.FirstOrDefault(t => t.Guid == guid);
            if (transportation is null)
            {
                return RedirectToPage("/Reiseplan/Index");
            }

            // Manually update the properties that are allowed to change
            transportation.Type = Transportation.Type;
            transportation.BookingNumber = Transportation.BookingNumber;
            transportation.IsRoundTrip = Transportation.IsRoundTrip;
            transportation.Cost = Transportation.Cost;

            try
            {
                _db.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                ModelState.AddModelError("", "Fehler beim Schreiben in die Datenbank");
                return Page();
            }

            return RedirectToPage("/Reiseplan/Details", new { guid = transportation.Guid });
        }

    }
}
