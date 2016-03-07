using System.Linq;
using FoodManager.Model;
using FoodManager.Model.IHmac;
using FoodManager.OrmLite.DataBase;

namespace FoodManager.OrmLite.Hmac
{
    public class HmacHelperOrmLite : IHmacHelper
    {
        private readonly IDataBaseSqlServerOrmLite _dataBaseSqlServerOrmLite;

        public HmacHelperOrmLite(IDataBaseSqlServerOrmLite dataBaseSqlServerOrmLite)
        {
            _dataBaseSqlServerOrmLite = dataBaseSqlServerOrmLite;
        }
        
        public User FindUserByPublicKey(string publicKey)
        {
            return _dataBaseSqlServerOrmLite.FindBy<User>(user => user.PublicKey == publicKey).FirstOrDefault();
        }

        public void RefreshHmacOfUser(User user)
        {
            _dataBaseSqlServerOrmLite.RefreshHmac<User>(user.PublicKey, user.Time, user.Id);
        }
    }
}