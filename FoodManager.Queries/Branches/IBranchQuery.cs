using System.Collections.Generic;
using FoodManager.Infrastructure.IGenericQuery;
using FoodManager.Model;

namespace FoodManager.Queries.Branches
{
    public interface IBranchQuery : IQuery
    {
        void WithOnlyActivated(bool onlyActivated);
        void WithOnlyStatusActivated(bool onlyStatusActivated);
        void WithOnlyStatusDeactivated(bool onlyStatusDeactivated);
        void WithCompany(int companyId);
        IEnumerable<Branch> Execute();
    }
}