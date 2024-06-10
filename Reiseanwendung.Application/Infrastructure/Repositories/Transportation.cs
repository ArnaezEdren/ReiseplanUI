using Reiseanwendung.Application.Model;

namespace Reiseanwendung.Application.Infrastructure.Repositories
{
    public class TransportationRepository : Repository<Transportation, int>
    {
        public TransportationRepository(ReiseplanContext db) : base(db)
        {
        }
    }
}
