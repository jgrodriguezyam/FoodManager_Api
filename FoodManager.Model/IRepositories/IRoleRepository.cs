using System.Collections.Generic;
using FoodManager.Infrastructure.IGenericRepositories;

namespace FoodManager.Model.IRepositories
{
    public interface IRoleRepository : IRepository<Role>
    {
        bool IsReference(int roleId);
        IEnumerable<Role> FindAll();
    }
}