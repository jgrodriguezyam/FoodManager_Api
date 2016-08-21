using System.Collections.Generic;
using FoodManager.Infrastructure.IGenericQuery;
using FoodManager.Model;

namespace FoodManager.Queries.Menus
{
    public interface IMenuQuery : IQuery
    {
        void WithOnlyActivated(bool onlyActivated);
        void WithOnlyStatusActivated(bool onlyStatusActivated);
        void WithOnlyStatusDeactivated(bool onlyStatusDeactivated);
        void WithDealer(int dealerId);
        void WithSaucer(int saucerId);
        void WithOnlyToday(bool onlyToday);
        void WithDaysWeek(string daysWeek);
        void WithDate(string date);
        IEnumerable<Menu> Execute();
    }
}