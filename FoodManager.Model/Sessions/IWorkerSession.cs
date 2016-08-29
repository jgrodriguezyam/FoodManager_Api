namespace FoodManager.Model.Sessions
{
    public interface IWorkerSession
    {
        Worker FindWorkerByPublicKey(string publicKey);
        void UpdateHmacOfWorker(Worker worker);
    }
}