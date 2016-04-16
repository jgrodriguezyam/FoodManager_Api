using System.Collections.Generic;
using FoodManager.Infrastructure.Integers;
using FoodManager.Infrastructure.Queries;
using FoodManager.Model;
using FoodManager.OrmLite.DataBase;
using FoodManager.OrmLite.Utils;
using ServiceStack.OrmLite.SqlServer;

namespace FoodManager.Queries.RoleConfigurations
{
    public class RoleConfigurationQuery : IRoleConfigurationQuery
    {
        private readonly SqlServerExpressionVisitor<RoleConfiguration> _query;
        private readonly IDataBaseSqlServerOrmLite _dataBaseSqlServerOrmLite;

        public RoleConfigurationQuery(IDataBaseSqlServerOrmLite dataBaseSqlServerOrmLite)
        {
            _dataBaseSqlServerOrmLite = dataBaseSqlServerOrmLite;
            _query = new SqlServerExpressionVisitor<RoleConfiguration>();
        }
        
        public void WithRole(int roleId)
        {
            if (roleId.IsNotZero())
                _query.Where(roleConfiguration => roleConfiguration.RoleId == roleId);
        }

        public void WithPermission(int permissionId)
        {
            if (permissionId.IsNotZero())
                _query.Where(roleConfiguration => roleConfiguration.PermissionId == permissionId);
        }

        public void WithAccessLevel(int accessLevelId)
        {
            if (accessLevelId.IsNotZero())
                _query.Where(roleConfiguration => roleConfiguration.AccessLevelId == accessLevelId);
        }

        public void Sort(string sort, string sortBy)
        {
            sort = sort.SortResolver();
            var property = sortBy.SortByPropertyResolver<RoleConfiguration>();

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

        public IEnumerable<RoleConfiguration> Execute()
        {
            return _dataBaseSqlServerOrmLite.FindExpressionVisitor(_query);
        }
    }
}