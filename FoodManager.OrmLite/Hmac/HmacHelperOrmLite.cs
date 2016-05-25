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

        public void UpdateHmacOfUser(User user)
        {
            _dataBaseSqlServerOrmLite.UpdateHmac<User>(user.PublicKey, user.Time, user.Id);
        }

        public Worker FindWorkerByPublicKey(string publicKey)
        {
            return _dataBaseSqlServerOrmLite.FindBy<Worker>(worker => worker.PublicKey == publicKey).FirstOrDefault();
        }

        public void UpdateHmacOfWorker(Worker worker)
        {
            _dataBaseSqlServerOrmLite.UpdateHmac<Worker>(worker.PublicKey, worker.Time, worker.Id);
        }
    }
}