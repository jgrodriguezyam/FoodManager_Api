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

namespace FoodManager.Queries.Ingredients
{
    public class IngredientQuery : IIngredientQuery
    {
        private readonly SqlServerExpressionVisitor<Ingredient> _query;
        private readonly IDataBaseSqlServerOrmLite _dataBaseSqlServerOrmLite;

        public IngredientQuery(IDataBaseSqlServerOrmLite dataBaseSqlServerOrmLite)
        {
            _dataBaseSqlServerOrmLite = dataBaseSqlServerOrmLite;
            _query = new SqlServerExpressionVisitor<Ingredient>();
        }

        public void WithOnlyActivated(bool onlyActivated)
        {
            if (onlyActivated)
                _query.Where(ingredient => ingredient.IsActive);
        }

        public void WithOnlyStatusActivated(bool onlyStatusActivated)
        {
            if (onlyStatusActivated)
                _query.Where(ingredient => ingredient.Status);
        }

        public void WithOnlyStatusDeactivated(bool onlyStatusDeactivated)
        {
            if (onlyStatusDeactivated)
                _query.Where(ingredient => ingredient.Status == GlobalConstants.StatusDeactivated);
        }

        public void WithIngredientGroup(int ingredientGroupId)
        {
            if (ingredientGroupId.IsNotZero())
                _query.Where(ingredient => ingredient.IngredientGroupId == ingredientGroupId);
        }

        public void WithName(string name)
        {
            if (name.IsNotNullOrEmpty())
                _query.Where(ingredient => ingredient.Name.Contains(name));
        }

        public void WithoutIds(string ids)
        {
            if (ids.IsNotNullOrEmpty())
                _query.Where(ingredient => !Sql.In(ingredient.Id, ids.Split(',')));
        }

        public void WithUnit(int unit)
        {
            if (unit.IsNotZero())
                _query.Where(ingredient => ingredient.Unit == unit);
        }

        public void WithIds(string ids)
        {
            if (ids.IsNotNullOrEmpty())
                _query.Where(ingredient => Sql.In(ingredient.Id, ids.Split(',')));
        }

        public void Sort(string sort, string sortBy)
        {
            sort = sort.SortResolver();
            var property = sortBy.SortByPropertyResolver<Ingredient>();

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

        public IEnumerable<Ingredient> Execute()
        {
            return _dataBaseSqlServerOrmLite.FindExpressionVisitor(_query);
        }
    }
}