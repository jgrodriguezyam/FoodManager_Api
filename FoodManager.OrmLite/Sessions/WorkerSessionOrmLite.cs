using System.Linq;
using FoodManager.Model;
using FoodManager.Model.Sessions;
using FoodManager.OrmLite.DataBase;

namespace FoodManager.OrmLite.Sessions
{
    public class WorkerSessionOrmLite : IWorkerSession
    {
        private readonly IDataBaseSqlServerOrmLite _dataBaseSqlServerOrmLite;

        public WorkerSessionOrmLite(IDataBaseSqlServerOrmLite dataBaseSqlServerOrmLite)
        {
            _dataBaseSqlServerOrmLite = dataBaseSqlServerOrmLite;
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