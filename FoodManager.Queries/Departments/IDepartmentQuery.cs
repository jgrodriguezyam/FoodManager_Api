using System.Collections.Generic;
using FoodManager.Infrastructure.IGenericQuery;
using FoodManager.Model;

namespace FoodManager.Queries.Departments
{
    public interface IDepartmentQuery : IQuery
    {
        void WithOnlyActivated(bool onlyActivated);
        IEnumerable<Department> Execute();
    }
}