using System.Collections.Generic;
using FoodManager.Infrastructure.Constants;
using FoodManager.Infrastructure.Enums;
using FoodManager.Infrastructure.Http;
using FoodManager.Infrastructure.Integers;
using FoodManager.Infrastructure.Queries;
using FoodManager.Infrastructure.Strings;
using FoodManager.Infrastructure.Validators.Enums;
using FoodManager.Model;
using FoodManager.Model.Sessions;
using FoodManager.OrmLite.DataBase;
using FoodManager.OrmLite.Utils;
using ServiceStack.OrmLite.SqlServer;

namespace FoodManager.Queries.Users
{
    public class UserQuery : IUserQuery
    {
        private readonly SqlServerExpressionVisitor<User> _query;
        private readonly IDataBaseSqlServerOrmLite _dataBaseSqlServerOrmLite;
        private readonly IUserSession _userSession;

        public UserQuery(IDataBaseSqlServerOrmLite dataBaseSqlServerOrmLite, IUserSession userSession)
        {
            _dataBaseSqlServerOrmLite = dataBaseSqlServerOrmLite;
            _userSession = userSession;
            _query = new SqlServerExpressionVisitor<User>();
        }

        public void WithOnlyActivated(bool onlyActivated)
        {
            if (onlyActivated)
                _query.Where(user => user.IsActive);
        }

        public void WithOnlyStatusActivated(bool onlyStatusActivated)
        {
            if (onlyStatusActivated)
                _query.Where(user => user.Status);
        }

        public void WithOnlyStatusDeactivated(bool onlyStatusDeactivated)
        {
            if (onlyStatusDeactivated)
                _query.Where(user => user.Status == GlobalConstants.StatusDeactivated);
        }

        public void WithDealer(int dealerId)
        {
            if (dealerId.IsNotZero())
                _query.Where(user => user.DealerId == dealerId);
        }

        public void WithName(string name)
        {
            if (name.IsNotNullOrEmpty())
                _query.Where(user => user.Name.Contains(name));
        }

        public void WithUserName(string userName)
        {
            if (userName.IsNotNullOrEmpty())
                _query.Where(user => user.UserName == userName);
        }

        public void WithOnlyBelongUser(bool onlyBelongUser)
        {
            if (onlyBelongUser)
            {
                var loginType = HttpContextAccess.GetLoginType();
                var publicKey = HttpContextAccess.GetPublicKey();
                var userSession = _userSession.FindUserByPublicKey(publicKey);
                    if (loginType.Value != LoginType.Worker.GetValue() && userSession.RoleId != GlobalConstants.AdminRoleId)
                    _query.Where(user => user.DealerId == userSession.DealerId);
            }
        }

        public void Sort(string sort, string sortBy)
        {
            sort = sort.SortResolver();
            var property = sortBy.SortByPropertyResolver<User>();

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

        public IEnumerable<User> Execute()
        {
            return _dataBaseSqlServerOrmLite.FindExpressionVisitor(_query);
        }
    }
}