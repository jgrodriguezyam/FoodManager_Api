namespace FoodManager.Model.IHmac
{
    public interface IHmacHelper
    {
        User FindUserByPublicKey(string publicKey);
        void UpdateHmacOfUser(User user);
        Worker FindWorkerByPublicKey(string publicKey);
        void UpdateHmacOfWorker(Worker worker);
    }
}