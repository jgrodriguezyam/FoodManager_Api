using System;
using System.Collections.Generic;
using FoodManager.Infrastructure.Constants;
using FoodManager.Infrastructure.Dates.Enums;
using FoodManager.Infrastructure.Enums;
using FoodManager.Infrastructure.Integers;
using FoodManager.Infrastructure.Queries;
using FoodManager.Model;
using FoodManager.OrmLite.DataBase;
using FoodManager.OrmLite.Utils;
using ServiceStack.OrmLite.SqlServer;

namespace FoodManager.Queries.Menus
{
    public class MenuQuery : IMenuQuery
    {
        private readonly SqlServerExpressionVisitor<Menu> _query;
        private readonly IDataBaseSqlServerOrmLite _dataBaseSqlServerOrmLite;

        public MenuQuery(IDataBaseSqlServerOrmLite dataBaseSqlServerOrmLite)
        {
            _dataBaseSqlServerOrmLite = dataBaseSqlServerOrmLite;
            _query = new SqlServerExpressionVisitor<Menu>();
        }

        public void WithOnlyActivated(bool onlyActivated)
        {
            if (onlyActivated)
                _query.Where(menu => menu.IsActive);
        }

        public void WithOnlyStatusActivated(bool onlyStatusActivated)
        {
            if (onlyStatusActivated)
                _query.Where(menu => menu.Status);
        }

        public void WithOnlyStatusDeactivated(bool onlyStatusDeactivated)
        {
            if (onlyStatusDeactivated)
                _query.Where(menu => menu.Status == GlobalConstants.StatusDeactivated);
        }

        public void WithDealer(int dealerId)
        {
            if (dealerId.IsNotZero())
                _query.Where(menu => menu.DealerId == dealerId);
        }

        public void WithSaucer(int saucerId)
        {
            if (saucerId.IsNotZero())
                _query.Where(menu => menu.SaucerId == saucerId);
        }

        public void WithOnlyToday(bool onlyToday)
        {
            if (onlyToday)
            {
                var today = DateTime.Now.Date;
                _query.Where(menu => menu.StartDate <= today && menu.EndDate >= today);
                var dayOfWeek = (int) today.DayOfWeek;
                _query.Where(menu => menu.DayWeek == DayWeek.All.GetValue() || menu.DayWeek == dayOfWeek);
            }
        }

        public void Sort(string sort, string sortBy)
        {
            sort = sort.SortResolver();
            var property = sortBy.SortByPropertyResolver<Menu>();

            if (sort.Equals(QueryConstants.OrderByDescending))
                _query.OrderByDescending(property);

            if (sort.Equals(QueryConstants.OrderByAscending))
                _query.OrderBy(property);
        }

        public void Paginate(int startPage, int endPage)
        {
            if (startPage.IsNotZero() && endPage.IsNotZero())
                _query.Limit(startPage.ConvertSkip(), startPage.ConvertRows(endPage));
        }

        public int TotalRecords()
        {
            return _dataBaseSqlServerOrmLite.Count(_query);
        }

        public IEnumerable<Menu> Execute()
        {
            return _dataBaseSqlServerOrmLite.FindExpressionVisitor(_query);
        }
    }
}