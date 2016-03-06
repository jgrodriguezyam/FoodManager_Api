using System.Collections.Generic;
using FoodManager.Infrastructure.IGenericQuery;
using FoodManager.Model;

namespace FoodManager.Queries.Companies
{
    public interface ICompanyQuery : IQuery
    {
        void WithOnlyActivated(bool onlyActivated);
        IEnumerable<Company> Execute();
    }
}