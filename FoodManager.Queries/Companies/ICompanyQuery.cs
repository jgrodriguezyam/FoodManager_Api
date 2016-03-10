using System.Collections.Generic;
using FoodManager.Infrastructure.IGenericQuery;
using FoodManager.Model;

namespace FoodManager.Queries.Companies
{
    public interface ICompanyQuery : IQuery
    {
        void WithOnlyActivated(bool onlyActivated);
        void WithOnlyStatusActivated(bool onlyStatusActivated);
        void WithOnlyStatusDeactivated(bool onlyStatusDeactivated);
        void WithRegion(int regionId);
        IEnumerable<Company> Execute();
    }
}