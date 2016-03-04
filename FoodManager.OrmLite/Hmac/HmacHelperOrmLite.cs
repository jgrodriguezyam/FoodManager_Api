using FoodManager.Model;
using FoodManager.Model.IHmac;

namespace FoodManager.OrmLite.Hmac
{
    public class HmacHelperOrmLite : IHmacHelper
    {
        public User FindUserByPublicKey(string publicKey)
        {
            throw new System.NotImplementedException();
        }

        public void RefreshHmacOfUser(User user)
        {
            throw new System.NotImplementedException();
        }
    }
}