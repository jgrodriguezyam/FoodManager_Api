using System.Collections.Generic;
using FoodManager.Infrastructure.IGenericQuery;
using FoodManager.Model;

namespace FoodManager.Queries.Regions
{
    public interface IRegionQuery : IQuery
    {
        void WithOnlyActivated(bool onlyActivated);
        IEnumerable<Region> Execute();
    }
}