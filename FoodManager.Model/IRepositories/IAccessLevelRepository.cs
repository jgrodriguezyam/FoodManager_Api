using System.Collections.Generic;
using FoodManager.Infrastructure.IGenericRepositories;

namespace FoodManager.Model.IRepositories
{
    public interface IAccessLevelRepository : IRepository<AccessLevel>
    {
        IEnumerable<AccessLevel> FindAll();
    }
}