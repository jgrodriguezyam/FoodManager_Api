using FoodManager.Infrastructure.IGenericRepositories;

namespace FoodManager.Model.IRepositories
{
    public interface IDepartmentRepository : IRepository<Department>
    {
        bool IsReference(int departmentId);
    }
}