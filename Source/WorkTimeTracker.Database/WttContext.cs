using Microsoft.EntityFrameworkCore;
using WorkTimeTracker.Database.Entities;

namespace WorkTimeTracker.Database
{
    public class WttContext : DbContext
    {
        public WttContext(DbContextOptions<WttContext> options) : base(options)
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
            DbPath = Path.Combine(path, "WorkTimeTracker", "blogging.db");
        }
    }
}
