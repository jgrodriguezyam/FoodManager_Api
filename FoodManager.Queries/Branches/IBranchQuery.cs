using System.Collections.Generic;
using FoodManager.Infrastructure.IGenericQuery;
using FoodManager.Model;

namespace FoodManager.Queries.Branches
{
    public interface IBranchQuery : IQuery
    {
        void WithOnlyActivated(bool onlyActivated);
        IEnumerable<Branch> Execute();
    }
}