using Microsoft.EntityFrameworkCore;
using WorkTimeTracker.Database.Entity;
using WorkTimeTracker.Interfaces.Repository;

namespace WorkTimeTracker.Database.Repository
{
    public class DayRepository : IBaseRepository<DayEntity>
    {
        bool disposed = false;
        readonly WttContext _context;

        public DayRepository()
        {
            _context = new WttContext();
        }

        public DayRepository(WttContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IEnumerable<DayEntity> GetAll()
        {
            return _context.Days.ToList();
        }

        public DayEntity GetById(int entityId)
        {
            return _context.Days.Find(entityId);
        }

        public void Insert(DayEntity entity)
        {
            _context.Days.Add(entity);
        }

        public void Update(DayEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(int entityId)
        {
            var day = _context.Days.Find(entityId);
            _context.Days.Remove(day);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
