using System.Collections.Generic;
using FoodManager.Infrastructure.IGenericQuery;
using FoodManager.Model;

namespace FoodManager.Queries.Departments
{
    public interface IDepartmentQuery : IQuery
    {
        void WithOnlyActivated(bool onlyActivated);
        void WithOnlyStatusActivated(bool onlyStatusActivated);
        void WithOnlyStatusDeactivated(bool onlyStatusDeactivated);
        void WithBranch(int branchId);
        IEnumerable<Department> Execute();
    }
}