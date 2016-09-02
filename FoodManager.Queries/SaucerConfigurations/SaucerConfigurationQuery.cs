using System.Collections.Generic;
using FoodManager.Infrastructure.Constants;
using FoodManager.Infrastructure.Integers;
using FoodManager.Infrastructure.Queries;
using FoodManager.Infrastructure.Strings;
using FoodManager.Model;
using FoodManager.OrmLite.DataBase;
using FoodManager.OrmLite.Utils;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.SqlServer;

namespace FoodManager.Queries.SaucerConfigurations
{
    public class SaucerConfigurationQuery : ISaucerConfigurationQuery
    {
        private readonly SqlServerExpressionVisitor<SaucerConfiguration> _query;
        private readonly IDataBaseSqlServerOrmLite _dataBaseSqlServerOrmLite;

        public SaucerConfigurationQuery(IDataBaseSqlServerOrmLite dataBaseSqlServerOrmLite)
        {
            _dataBaseSqlServerOrmLite = dataBaseSqlServerOrmLite;
            _query = new SqlServerExpressionVisitor<SaucerConfiguration>();
        }

        public void WithOnlyActivated(bool onlyActivated)
        {
            if (onlyActivated)
                _query.Where(saucerConfiguration => saucerConfiguration.IsActive);
        }

        public void WithOnlyStatusActivated(bool onlyStatusActivated)
        {
            if (onlyStatusActivated)
                _query.Where(saucerConfiguration => saucerConfiguration.Status);
        }

        public void WithOnlyStatusDeactivated(bool onlyStatusDeactivated)
        {
            if (onlyStatusDeactivated)
                _query.Where(saucerConfiguration => saucerConfiguration.Status == GlobalConstants.StatusDeactivated);
        }

        public void WithSaucer(int saucerId)
        {
            if (saucerId.IsNotZero())
                _query.Where(saucerConfiguration => saucerConfiguration.SaucerId == saucerId);
        }

        public void WithSaucerIds(string saucerIds)
        {
            if (saucerIds.IsNotNullOrEmpty())
                _query.Where(saucerConfiguration => Sql.In(saucerConfiguration.SaucerId, saucerIds.Split(',')));
        }

        public void WithIngredient(int ingredientId)
        {
            if (ingredientId.IsNotZero())
                _query.Where(saucerConfiguration => saucerConfiguration.IngredientId == ingredientId);
        }

        public void Sort(string sort, string sortBy)
        {
            sort = sort.SortResolver();
            var property = sortBy.SortByPropertyResolver<SaucerConfiguration>();

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

        public IEnumerable<SaucerConfiguration> Execute()
        {
            return _dataBaseSqlServerOrmLite.FindExpressionVisitor(_query);
        }
    }
}