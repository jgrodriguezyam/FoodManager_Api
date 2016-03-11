using System.Collections.Generic;
using FoodManager.Infrastructure.IGenericQuery;
using FoodManager.Model;

namespace FoodManager.Queries.Tips
{
    public interface ITipQuery : IQuery
    {
        void WithOnlyActivated(bool onlyActivated);
        void WithOnlyStatusActivated(bool onlyStatusActivated);
        void WithOnlyStatusDeactivated(bool onlyStatusDeactivated);
        IEnumerable<Tip> Execute();
    }
}