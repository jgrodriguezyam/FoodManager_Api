using FoodManager.Infrastructure.IGenericRepositories;

namespace FoodManager.Model.IRepositories
{
    public interface IDealerRepository : IRepository<Dealer>
    {
        bool IsReference(int dealerId);
    }
}