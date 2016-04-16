using System.Collections.Generic;
using FoodManager.Infrastructure.IGenericQuery;
using FoodManager.Model;

namespace FoodManager.Queries.RoleConfigurations
{
    public interface IRoleConfigurationQuery : IQuery
    {
        void WithRole(int roleId);
        void WithPermission(int permissionId);
        void WithAccessLevel(int accessLevelId);
        IEnumerable<RoleConfiguration> Execute();
    }
}