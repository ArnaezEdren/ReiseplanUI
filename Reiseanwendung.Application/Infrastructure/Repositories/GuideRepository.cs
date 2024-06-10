using Reiseanwendung.Application.Model;

namespace Reiseanwendung.Application.Infrastructure.Repositories
{
    public class GuideRepository : Repository<Guide, int>
    {
        public GuideRepository(ReiseplanContext db) : base(db)
        {
        }
    }
}
