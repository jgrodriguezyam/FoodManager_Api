using FoodManager.Infrastructure.IGenericRepositories;

namespace FoodManager.Model.IRepositories
{
    public interface IWorkerRepository : IRepository<Worker>
    {
        bool IsReference(int workerId);
    }
}