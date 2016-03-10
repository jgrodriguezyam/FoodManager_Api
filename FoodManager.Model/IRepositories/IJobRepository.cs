using FoodManager.Infrastructure.IGenericRepositories;

namespace FoodManager.Model.IRepositories
{
    public interface IJobRepository : IRepository<Job>
    {
        bool IsReference(int jobId);
    }
}