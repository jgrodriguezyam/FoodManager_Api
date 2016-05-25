using System.Collections.Generic;
using System.Linq;
using FoodManager.Infrastructure.Constants;
using FoodManager.Infrastructure.Integers;
using FoodManager.Infrastructure.Queries;
using FoodManager.Infrastructure.Strings;
using FoodManager.Model;
using FoodManager.OrmLite.DataBase;
using FoodManager.OrmLite.Utils;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.SqlServer;

namespace FoodManager.Queries.Saucers
{
    public class SaucerQuery : ISaucerQuery
    {
        private readonly SqlServerExpressionVisitor<Saucer> _query;
        private readonly IDataBaseSqlServerOrmLite _dataBaseSqlServerOrmLite;

        public SaucerQuery(IDataBaseSqlServerOrmLite dataBaseSqlServerOrmLite)
        {
            _dataBaseSqlServerOrmLite = dataBaseSqlServerOrmLite;
            _query = new SqlServerExpressionVisitor<Saucer>();
        }

        public void WithOnlyActivated(bool onlyActivated)
        {
            if (onlyActivated)
                _query.Where(saucer => saucer.IsActive);
        }

        public void WithOnlyStatusActivated(bool onlyStatusActivated)
        {
            if (onlyStatusActivated)
                _query.Where(saucer => saucer.Status);
        }

        public void WithOnlyStatusDeactivated(bool onlyStatusDeactivated)
        {
            if (onlyStatusDeactivated)
                _query.Where(saucer => saucer.Status == GlobalConstants.StatusDeactivated);
        }

        public void WithName(string name)
        {
            if (name.IsNotNullOrEmpty())
                _query.Where(saucer => saucer.Name.Contains(name));
        }

        public void WithDealer(int dealerId)
        {
            if (dealerId.IsNotZero())
            {
                var dealerSaucerQuery = new SqlServerExpressionVisitor<DealerSaucer>();
                dealerSaucerQuery.Where(dealerSaucer => dealerSaucer.DealerId == dealerId);
                var saucerIds = _dataBaseSqlServerOrmLite.FindExpressionVisitor(dealerSaucerQuery).Select(dealerSaucer => dealerSaucer.SaucerId);
                saucerIds = saucerIds.Count().IsNotZero() ? saucerIds : new[] { int.MinValue };
                _query.Where(saucer => Sql.In(saucer.Id, saucerIds));
            }
        }

        public void WithoutDealer(int dealerId)
        {
            if (dealerId.IsNotZero())
            {
                var dealerSaucerQuery = new SqlServerExpressionVisitor<DealerSaucer>();
                dealerSaucerQuery.Where(dealerSaucer => dealerSaucer.DealerId == dealerId);
                var saucerIds = _dataBaseSqlServerOrmLite.FindExpressionVisitor(dealerSaucerQuery).Select(dealerSaucer => dealerSaucer.SaucerId);
                saucerIds = saucerIds.Count().IsNotZero() ? saucerIds : new[] { int.MinValue };
                _query.Where(saucer => !Sql.In(saucer.Id, saucerIds));
            }
        }

        public void Sort(string sort, string sortBy)
        {
            sort = sort.SortResolver();
            var property = sortBy.SortByPropertyResolver<Saucer>();

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

        public IEnumerable<Saucer> Execute()
        {
            return _dataBaseSqlServerOrmLite.FindExpressionVisitor(_query);
        }
    }
}