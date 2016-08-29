namespace FoodManager.Model.Sessions
{
    public interface IUserSession
    {
        User FindUserByPublicKey(string publicKey);
        void UpdateHmacOfUser(User user);
    }
}