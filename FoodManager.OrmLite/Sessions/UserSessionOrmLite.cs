using System.Linq;
using FoodManager.Model;
using FoodManager.Model.Sessions;
using FoodManager.OrmLite.DataBase;

namespace FoodManager.OrmLite.Sessions
{
    public class UserSessionOrmLite : IUserSession
    {
        private readonly IDataBaseSqlServerOrmLite _dataBaseSqlServerOrmLite;

        public UserSessionOrmLite(IDataBaseSqlServerOrmLite dataBaseSqlServerOrmLite)
        {
            _dataBaseSqlServerOrmLite = dataBaseSqlServerOrmLite;
        }

        public User FindUserByPublicKey(string publicKey)
        {
            return _dataBaseSqlServerOrmLite.FindBy<User>(user => user.PublicKey == publicKey).FirstOrDefault();
        }

        public void UpdateHmacOfUser(User user)
        {
            _dataBaseSqlServerOrmLite.UpdateHmac<User>(user.PublicKey, user.Time, user.Id);
        }
    }
}