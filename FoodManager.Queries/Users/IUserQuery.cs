using System.Collections.Generic;
using FoodManager.Infrastructure.IGenericQuery;
using FoodManager.Model;

namespace FoodManager.Queries.Users
{
    public interface IUserQuery : IQuery
    {
        void WithOnlyActivated(bool onlyActivated);
        void WithOnlyStatusActivated(bool onlyStatusActivated);
        void WithOnlyStatusDeactivated(bool onlyStatusDeactivated);
        void WithDealer(int dealerId);
        void WithName(string name);
        void WithUserName(string userName);
        void WithOnlyBelongUser(bool onlyBelongUser);
        IEnumerable<User> Execute();
    }
}