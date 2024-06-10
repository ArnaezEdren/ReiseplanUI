using Reiseanwendung.Application.Model;

namespace Reiseanwendung.Application.Infrastructure.Repositories
{
    public class AddressRepository : Repository<Address, int>
    {
        public AddressRepository(ReiseplanContext db) : base(db)
        {
        }
    }
}
