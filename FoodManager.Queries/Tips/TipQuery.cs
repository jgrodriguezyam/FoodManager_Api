using System.Collections.Generic;
using FoodManager.Infrastructure.Constants;
using FoodManager.Infrastructure.Integers;
using FoodManager.Infrastructure.Queries;
using FoodManager.Model;
using FoodManager.OrmLite.DataBase;
using FoodManager.OrmLite.Utils;
using ServiceStack.OrmLite.SqlServer;

namespace FoodManager.Queries.Tips
{
    public class TipQuery : ITipQuery
    {
        private readonly SqlServerExpressionVisitor<Tip> _query;
        private readonly IDataBaseSqlServerOrmLite _dataBaseSqlServerOrmLite;

        public TipQuery(IDataBaseSqlServerOrmLite dataBaseSqlServerOrmLite)
        {
            _dataBaseSqlServerOrmLite = dataBaseSqlServerOrmLite;
            _query = new SqlServerExpressionVisitor<Tip>();
        }

        public void WithOnlyActivated(bool onlyActivated)
        {
            if (onlyActivated)
                _query.Where(tip => tip.IsActive);
        }

        public void WithOnlyStatusActivated(bool onlyStatusActivated)
        {
            if (onlyStatusActivated)
                _query.Where(tip => tip.Status);
        }

        public void WithOnlyStatusDeactivated(bool onlyStatusDeactivated)
        {
            if (onlyStatusDeactivated)
                _query.Where(tip => tip.Status == GlobalConstants.StatusDeactivated);
        }

        public void Sort(string sort, string sortBy)
        {
            sort = sort.SortResolver();
            var property = sortBy.SortByPropertyResolver<Tip>();

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

        public IEnumerable<Tip> Execute()
        {
            return _dataBaseSqlServerOrmLite.FindExpressionVisitor(_query);
        }
    }
}