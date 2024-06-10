using Reiseanwendung.Application.Model;

namespace Reiseanwendung.Application.Infrastructure.Repositories
{
    public class PersonRepository : Repository<Person, int>
    {
        public PersonRepository(ReiseplanContext db) : base(db)
        {
        }
    }
}
