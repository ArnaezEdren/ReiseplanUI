using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Reiseanwendung.Application.Infrastructure.Repositories;
using Reiseanwendung.Application.Model;
using System.Collections.Generic;
using System.Linq;

namespace Reiseanwendung.Webapp.Pages.Reiseplan
{
    public class IndexModel : PageModel
    {
        private readonly ReiseplanRepository _reiseplanRepository;

        public IndexModel(ReiseplanRepository reiseplanRepository)
        {
            _reiseplanRepository = reiseplanRepository;
        }

        public List<TravelPlan> Travelplans { get; private set; } = new List<TravelPlan>();

        public void OnGet()
        {
            Travelplans = _reiseplanRepository.Set
                .Include(tp => tp.Destinations)
                    .ThenInclude(d => d.Activities)
                .Include(tp => tp.Destinations)
                    .ThenInclude(d => d.Accommodations)
                .Include(tp => tp.Destinations)
                    .ThenInclude(d => d.Transportations)
                    .OrderByDescending(d => d.StartDate)
              
                .ToList();
        }
    }
}
