using Microsoft.EntityFrameworkCore;
using Reiseanwendung.Application.Infrastructure;
using Reiseanwendung.Application.Model;
using System;
using System.Linq;

namespace Reiseanwendung.Application.Infrastructure.Repositories
{
    public abstract class Repository<Tentity, Tkey> where Tentity : class, IEntity<Tkey> where Tkey : struct
    {
        protected readonly ReiseplanContext _db;

        public IQueryable<Tentity> Set => _db.Set<Tentity>();

        protected Repository(ReiseplanContext db)
        {
            _db = db;
        }

        public Tentity? FindByGuid(Guid guid) => _db.Set<Tentity>().FirstOrDefault(t => t.Guid.Equals(guid));
        public Tentity? FindById(Guid guid) => _db.Set<Tentity>().FirstOrDefault(t => t.Id.Equals(guid));

        public virtual (bool success, string message) Insert(Tentity entity)
        {
            _db.Entry(entity).State = EntityState.Added;
            try
            {
                _db.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                return (false, ex.InnerException?.Message ?? ex.Message);
            }
            return (true, string.Empty);
        }

        public virtual (bool success, string message) Update(Tentity entity)
        {
            if (entity.Id.Equals(default)) { return (false, "Missing primary key."); }
            _db.Entry(entity).State = EntityState.Modified;
            try
            {
                _db.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                return (false, ex.InnerException?.Message ?? ex.Message);
            }
            return (true, string.Empty);
        }

        public virtual (bool success, string message) Delete(Tentity entity)
        {
            if (entity.Id.Equals(default)) { return (false, "Missing primary key."); }
            _db.Entry(entity).State = EntityState.Deleted;
            try
            {
                _db.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                return (false, ex.InnerException?.Message ?? ex.Message);
            }
            return (true, string.Empty);
        }
    }

    public class ReiseplanRepository : Repository<TravelPlan, int>
    {
        public ReiseplanRepository(ReiseplanContext db) : base(db)
        {
        }
    }
}
