namespace FoodManager.Infrastructure.IGenericRepositories
{
    public interface IWritableRepository<in T>
    {
        void Add(T item);
        void Update(T item);
        void Remove(T item);
    }
}