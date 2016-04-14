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

namespace FoodManager.Queries.Dealers
{
    public class DealerQuery : IDealerQuery
    {
        private readonly SqlServerExpressionVisitor<Dealer> _query;
        private readonly IDataBaseSqlServerOrmLite _dataBaseSqlServerOrmLite;

        public DealerQuery(IDataBaseSqlServerOrmLite dataBaseSqlServerOrmLite)
        {
            _dataBaseSqlServerOrmLite = dataBaseSqlServerOrmLite;
            _query = new SqlServerExpressionVisitor<Dealer>();
        }

        public void WithOnlyActivated(bool onlyActivated)
        {
            if (onlyActivated)
                _query.Where(dealer => dealer.IsActive);
        }

        public void WithOnlyStatusActivated(bool onlyStatusActivated)
        {
            if (onlyStatusActivated)
                _query.Where(dealer => dealer.Status);
        }

        public void WithOnlyStatusDeactivated(bool onlyStatusDeactivated)
        {
            if (onlyStatusDeactivated)
                _query.Where(dealer => dealer.Status == GlobalConstants.StatusDeactivated);
        }

        public void WithName(string name)
        {
            if (name.IsNotNullOrEmpty())
                _query.Where(dealer => dealer.Name.Contains(name));
        }

        public void WithBranch(int branchId)
        {
            if (branchId.IsNotZero())
            {
                var branchDealerQuery = new SqlServerExpressionVisitor<BranchDealer>();
                branchDealerQuery.Where(branchDealer => branchDealer.BranchId == branchId);
                var dealerIds = _dataBaseSqlServerOrmLite.FindExpressionVisitor(branchDealerQuery).Select(branchDealer => branchDealer.DealerId);
                dealerIds = dealerIds.Count().IsNotZero() ? dealerIds : new [] { int.MinValue };
                _query.Where(dealer => Sql.In(dealer.Id, dealerIds));
            }
        }

        public void Sort(string sort, string sortBy)
        {
            sort = sort.SortResolver();
            var property = sortBy.SortByPropertyResolver<Dealer>();

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

        public IEnumerable<Dealer> Execute()
        {
            return _dataBaseSqlServerOrmLite.FindExpressionVisitor(_query);
        }
    }
}