using Microsoft.EntityFrameworkCore;
using WorkTimeTracker.Database.Entity;

namespace WorkTimeTracker.Database
{
    public class WttContext : DbContext
    {
        public WttContext()
        {
            SetDbPath();
        }

        public string DbPath { get; private set; }

        public DbSet<DayEntity> Days { get; set; }

        public DbSet<TaskEntity> Tasks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlite($"Data Source={DbPath}");

        protected override void OnModelCreating(ModelBuilder modelBuilder) => modelBuilder.ApplyConfigurationsFromAssembly(typeof(WttContext).Assembly);

        void SetDbPath()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);

            if(!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            DbPath = Path.Combine(path, "WorkTimeTracker", "worktimetracker.db");
        }
    }
}
