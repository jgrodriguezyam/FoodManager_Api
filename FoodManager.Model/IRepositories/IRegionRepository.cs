using FoodManager.Infrastructure.IGenericRepositories;

namespace FoodManager.Model.IRepositories
{
    public interface IRegionRepository : IRepository<Region>
    {
        bool IsReference(int regionId);
    }
}