using System.Collections.Generic;
using FoodManager.Model;

namespace FoodManager.Queries.AccessLevels
{
    public interface IAccessLevelQuery
    {
        void WithPermission(int permissionId);
        IEnumerable<AccessLevel> Execute();
    }
}