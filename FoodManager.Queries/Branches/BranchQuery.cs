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

namespace FoodManager.Queries.Branches
{
    public class BranchQuery : IBranchQuery
    {
        private readonly SqlServerExpressionVisitor<Branch> _query;
        private readonly IDataBaseSqlServerOrmLite _dataBaseSqlServerOrmLite;

        public BranchQuery(IDataBaseSqlServerOrmLite dataBaseSqlServerOrmLite)
        {
            _dataBaseSqlServerOrmLite = dataBaseSqlServerOrmLite;
            _query = new SqlServerExpressionVisitor<Branch>();
        }

        public void WithOnlyActivated(bool onlyActivated)
        {
            if (onlyActivated)
                _query.Where(branch => branch.IsActive);
        }

        public void WithOnlyStatusActivated(bool onlyStatusActivated)
        {
            if (onlyStatusActivated)
                _query.Where(branch => branch.Status);
        }

        public void WithOnlyStatusDeactivated(bool onlyStatusDeactivated)
        {
            if (onlyStatusDeactivated)
                _query.Where(branch => branch.Status == GlobalConstants.StatusDeactivated);
        }

        public void WithRegion(int regionId)
        {
            if (regionId.IsNotZero())
                _query.Where(branch => branch.RegionId == regionId);
        }

        public void WithCompany(int companyId)
        {
            if (companyId.IsNotZero())
                _query.Where(branch => branch.CompanyId == companyId);
        }

        public void WithName(string name)
        {
            if (name.IsNotNullOrEmpty())
                _query.Where(branch => branch.Name.Contains(name));
        }

        public void WithCode(string code)
        {
            if (code.IsNotNullOrEmpty())
                _query.Where(branch => branch.Code == code);
        }

        public void WithDealer(int dealerId)
        {
            if (dealerId.IsNotZero())
            {
                var branchDealerQuery = new SqlServerExpressionVisitor<BranchDealer>();
                branchDealerQuery.Where(branchDealer => branchDealer.DealerId == dealerId);
                var branchIds = _dataBaseSqlServerOrmLite.FindExpressionVisitor(branchDealerQuery).Select(branchDealer => branchDealer.BranchId);
                branchIds = branchIds.Count().IsNotZero() ? branchIds : new[] { int.MinValue };
                _query.Where(branch => Sql.In(branch.Id, branchIds));
            }
        }

        public void Sort(string sort, string sortBy)
        {
            sort = sort.SortResolver();
            var property = sortBy.SortByPropertyResolver<Branch>();

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

        public IEnumerable<Branch> Execute()
        {
            return _dataBaseSqlServerOrmLite.FindExpressionVisitor(_query);
        }
    }
}