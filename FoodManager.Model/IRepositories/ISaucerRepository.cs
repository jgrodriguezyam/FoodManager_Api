using FoodManager.Infrastructure.IGenericRepositories;

namespace FoodManager.Model.IRepositories
{
    public interface ISaucerRepository : IRepository<Saucer>
    {
        bool IsReference(int saucerId);
    }
}