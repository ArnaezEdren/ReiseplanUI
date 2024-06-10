using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Reiseanwendung.Application.Infrastructure;
using Reiseanwendung.Application.Model;
using Reiseanwendung.Webapp.Dto;
using Reiseanwendung.Webapp.Dto.Reiseanwendung.Webapp.TravelplanDto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Reiseanwendung.Webapp.Pages.Reiseplan
{
    public class CreateModel : PageModel
    {
        private readonly ReiseplanContext _db;
        private readonly IMapper _mapper;

        [BindProperty]
        public TravelplanDto NewTravelPlan { get; set; } = new TravelplanDto
        {
            Destinations = new List<DestinationDto>
            {
                new DestinationDto
                {
                    Activities = new List<ActivityDto>
                    {
                        new ActivityDto
                        {
                            StartDateTime = DateTime.Now,
                            EndDateTime = DateTime.Now
                        }
                    },
                    Accommodations = new List<AccommodationDto>
                    {
                        new AccommodationDto
                        {
                            Address = new AddressDto()
                        }
                    },
                    Transportations = new List<TransportationDto>()
                }
            },
            People = new List<PersonDto> { new PersonDto() }
        };

        public CreateModel(ReiseplanContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public void OnGet()
        {
            // Initialize the StartDate and EndDate to current date as default
            NewTravelPlan.StartDate = DateTime.Now;
            NewTravelPlan.EndDate = DateTime.Now;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var travelPlan = _mapper.Map<TravelPlan>(NewTravelPlan);

            _db.TravelPlans.Add(travelPlan);
            await _db.SaveChangesAsync();

            return RedirectToPage("/Reiseplan/Index");
        }
    }
}
