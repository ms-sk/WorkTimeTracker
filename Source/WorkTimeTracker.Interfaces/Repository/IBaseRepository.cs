namespace WorkTimeTracker.Interfaces.Repository
{
    public interface IBaseRepository<TEntity> : IDisposable
    {
        IEnumerable<TEntity> GetAll();

        TEntity GetById(int entityId);

        void Insert(TEntity entity);

        void Update(TEntity entity);

        void Delete(int entityId);

        void Save();
    }
}
