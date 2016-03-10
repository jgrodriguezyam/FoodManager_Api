using FoodManager.Infrastructure.IGenericRepositories;

namespace FoodManager.Model.IRepositories
{
    public interface IDiseaseRepository : IRepository<Disease>
    {
        bool IsReference(int diseaseId);
    }
}