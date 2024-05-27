using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Reiseanwendung.Application.Infrastructure;
using Reiseanwendung.Application.Model;


namespace Reiseanwendung.Webapp.Pages.Reiseplan
{
    public class IndexModel : PageModel
    {
        private readonly ReiseplanContext _db;
        public List<TravelPlan> Travelplans { get; set; } = new();

        public IndexModel(ReiseplanContext db)
        {
            _db = db;
        }

        public void OnGet()
        {
            
                Travelplans = _db.TravelPlans
                    .Include(tp => tp.Destinations)
                    .Include(tp => tp.People)
                    .OrderBy(t => t.Name)
                    .ToList();
            


        }
    }
}
