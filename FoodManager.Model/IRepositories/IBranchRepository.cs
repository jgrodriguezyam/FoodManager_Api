using FoodManager.Infrastructure.IGenericRepositories;

namespace FoodManager.Model.IRepositories
{
    public interface IBranchRepository : IRepository<Branch>
    {
        bool IsReference(int branchId);
    }
}