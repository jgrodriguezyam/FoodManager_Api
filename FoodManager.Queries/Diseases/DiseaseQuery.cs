using System.Collections.Generic;
using FoodManager.Infrastructure.Constants;
using FoodManager.Infrastructure.Integers;
using FoodManager.Infrastructure.Queries;
using FoodManager.Infrastructure.Strings;
using FoodManager.Model;
using FoodManager.OrmLite.DataBase;
using FoodManager.OrmLite.Utils;
using ServiceStack.OrmLite.SqlServer;

namespace FoodManager.Queries.Diseases
{
    public class DiseaseQuery : IDiseaseQuery
    {
        private readonly SqlServerExpressionVisitor<Disease> _query;
        private readonly IDataBaseSqlServerOrmLite _dataBaseSqlServerOrmLite;

        public DiseaseQuery(IDataBaseSqlServerOrmLite dataBaseSqlServerOrmLite)
        {
            _dataBaseSqlServerOrmLite = dataBaseSqlServerOrmLite;
            _query = new SqlServerExpressionVisitor<Disease>();
        }

        public void WithOnlyActivated(bool onlyActivated)
        {
            if (onlyActivated)
                _query.Where(disease => disease.IsActive);
        }

        public void WithOnlyStatusActivated(bool onlyStatusActivated)
        {
            if (onlyStatusActivated)
                _query.Where(disease => disease.Status);
        }

        public void WithOnlyStatusDeactivated(bool onlyStatusDeactivated)
        {
            if (onlyStatusDeactivated)
                _query.Where(disease => disease.Status == GlobalConstants.StatusDeactivated);
        }

        public void WithName(string name)
        {
            if (name.IsNotNullOrEmpty())
                _query.Where(disease => disease.Name.Contains(name));
        }

        public void Sort(string sort, string sortBy)
        {
            sort = sort.SortResolver();
            var property = sortBy.SortByPropertyResolver<Disease>();

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

        public IEnumerable<Disease> Execute()
        {
            return _dataBaseSqlServerOrmLite.FindExpressionVisitor(_query);
        }
    }
}