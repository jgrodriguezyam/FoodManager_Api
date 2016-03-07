using System.Collections.Generic;
using FoodManager.Infrastructure.IGenericQuery;
using FoodManager.Model;

namespace FoodManager.Queries.Users
{
    public interface IUserQuery : IQuery
    {
        void WithOnlyActivated(bool onlyActivated);
        IEnumerable<User> Execute();
    }
}