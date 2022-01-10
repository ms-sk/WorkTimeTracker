namespace Core.Storage
{
    public interface IStorage<T>
    {
        Task<T> Load();

        Task Save(T t);
    }
}