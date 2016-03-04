namespace FoodManager.Model.IHmac
{
    public interface IHmacHelper
    {
        User FindUserByPublicKey(string publicKey);
        void RefreshHmacOfUser(User user);
    }
}