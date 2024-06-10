using Reiseanwendung.Application.Model;

namespace Reiseanwendung.Application.Infrastructure.Repositories
{
    public class ActivityRepository : Repository<Activity, int>
    {
        public ActivityRepository(ReiseplanContext db) : base(db)
        {
        }
    }
}
