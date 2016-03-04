namespace FoodManager.Infrastructure.IGenericRepositories
{
    public interface IRepository<T> : IReadableRepository<T>, IWritableRepository<T>
    {
         
    }
}