using System.Collections.Generic;
using FoodManager.Infrastructure.Constants;
using FoodManager.Infrastructure.Integers;
using FoodManager.Infrastructure.Queries;
using FoodManager.Model;
using FoodManager.OrmLite.DataBase;
using FoodManager.OrmLite.Utils;
using ServiceStack.OrmLite.SqlServer;

namespace FoodManager.Queries.Companies
{
    public class CompanyQuery : ICompanyQuery
    {
        private readonly SqlServerExpressionVisitor<Company> _query;
        private readonly IDataBaseSqlServerOrmLite _dataBaseSqlServerOrmLite;

        public CompanyQuery(IDataBaseSqlServerOrmLite dataBaseSqlServerOrmLite)
        {
            _dataBaseSqlServerOrmLite = dataBaseSqlServerOrmLite;
            _query = new SqlServerExpressionVisitor<Company>();
        }

        public void WithOnlyActivated(bool onlyActivated)
        {
            if (onlyActivated)
                _query.Where(company => company.IsActive);
        }

        public void WithOnlyStatusActivated(bool onlyStatusActivated)
        {
            if (onlyStatusActivated)
                _query.Where(company => company.Status);
        }

        public void WithOnlyStatusDeactivated(bool onlyStatusDeactivated)
        {
            if (onlyStatusDeactivated)
                _query.Where(company => company.Status == GlobalConstants.StatusDeactivated);
        }

        public void WithRegion(int regionId)
        {
            if (regionId.IsNotZero())
                _query.Where(company => company.RegionId == regionId);
        }

        public void Sort(string sort, string sortBy)
        {
            sort = sort.SortResolver();
            var property = sortBy.SortByPropertyResolver<Company>();

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

        public IEnumerable<Company> Execute()
        {
            return _dataBaseSqlServerOrmLite.FindExpressionVisitor(_query);
        }
    }
}