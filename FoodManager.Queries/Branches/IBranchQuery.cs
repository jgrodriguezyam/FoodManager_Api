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
        void WithRegion(int regionId);
        void WithCompany(int companyId);
        void WithName(string name);
        void WithCode(string code);
        IEnumerable<Branch> Execute();
    }
}