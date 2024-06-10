using Reiseanwendung.Application.Model;

namespace Reiseanwendung.Application.Infrastructure.Repositories
{
    public class DestinationRepository : Repository<Destination, int>
    {
        public DestinationRepository(ReiseplanContext db) : base(db)
        {
        }
    }
}
