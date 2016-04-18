using System.Collections.Generic;
using System.Linq;
using FoodManager.Infrastructure.Integers;
using FoodManager.Model;
using FoodManager.OrmLite.DataBase;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.SqlServer;

namespace FoodManager.Queries.AccessLevels
{
    public class AccessLevelQuery : IAccessLevelQuery
    {
        private readonly SqlServerExpressionVisitor<AccessLevel> _query;
        private readonly IDataBaseSqlServerOrmLite _dataBaseSqlServerOrmLite;

        public AccessLevelQuery(IDataBaseSqlServerOrmLite dataBaseSqlServerOrmLite)
        {
            _dataBaseSqlServerOrmLite = dataBaseSqlServerOrmLite;
            _query = new SqlServerExpressionVisitor<AccessLevel>();
        }

        public void WithPermission(int permissionId)
        {
            if (permissionId.IsNotZero())
            {
                var permissionAccessLevelQuery = new SqlServerExpressionVisitor<PermissionAccessLevel>();
                permissionAccessLevelQuery.Where(permissionAccessLevel => permissionAccessLevel.PermissionId == permissionId);
                var accessLevelIds = _dataBaseSqlServerOrmLite.FindExpressionVisitor(permissionAccessLevelQuery).Select(permissionAccessLevel => permissionAccessLevel.AccessLevelId);
                accessLevelIds = accessLevelIds.Count().IsNotZero() ? accessLevelIds : new[] { int.MinValue };
                _query.Where(accessLevel => Sql.In(accessLevel.Id, accessLevelIds));
            }
        }

        public IEnumerable<AccessLevel> Execute()
        {
            return _dataBaseSqlServerOrmLite.FindExpressionVisitor(_query);
        }
    }
}