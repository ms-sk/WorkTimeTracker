using Microsoft.EntityFrameworkCore;
using WorkTimeTracker.Database.Entity;
using WorkTimeTracker.Interfaces.Repository;

namespace WorkTimeTracker.Database.Repository
{
    public class TaskRepository : IBaseRepository<TaskEntity>
    {
        bool disposed = false;
        readonly WttContext _context;

        public TaskRepository()
        {
            _context = new WttContext();
        }

        public TaskRepository(WttContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IEnumerable<TaskEntity> GetAll()
        {
            return _context.Tasks.ToList();
        }

        public TaskEntity? GetById(int taskId)
        {
            return _context.Tasks.Find(taskId);
        }

        public void Insert(TaskEntity task)
        {
            _context.Tasks.Add(task);
        }

        public void Update(TaskEntity task)
        {
            _context.Entry(task).State = EntityState.Modified;
        }

        public void Delete(int taskId)
        {
            var task = _context.Tasks.Find(taskId);
            _context.Tasks.Remove(task);
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
