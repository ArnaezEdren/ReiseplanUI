using Reiseanwendung.Application.Model;

namespace Reiseanwendung.Application.Infrastructure.Repositories
{
    public class BookingRepository : Repository<Booking, int>
    {
        public BookingRepository(ReiseplanContext db) : base(db)
        {
        }
    }
}
