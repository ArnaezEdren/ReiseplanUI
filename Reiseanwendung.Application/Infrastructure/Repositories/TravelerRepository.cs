using Reiseanwendung.Application.Model;

namespace Reiseanwendung.Application.Infrastructure.Repositories
{
    public class TravelerRepository : Repository<Traveler, int>
    {
        public TravelerRepository(ReiseplanContext db) : base(db)
        {
        }
    }
}
