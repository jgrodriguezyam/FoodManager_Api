using FoodManager.Infrastructure.Hmac;
using FoodManager.Infrastructure.Utils;
using FoodManager.Model.Base;

namespace FoodManager.Model
{
    public class User : EntityBase, IDeletable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public bool Status { get; set; }
        public int BranchId { get; set; }

        public string UserName { get; set; }
        public string Password { get; set; }
        public string PublicKey { get; set; }
        public string Time { get; set; }

        #region IDeletable

        public bool IsActive { get; set; }

        #endregion

        #region Methods

        public virtual void Login()
        {
            PublicKey = HmacGenerator.PublicKey();
            Time = HmacGenerator.Timespan();
        }

        public virtual void Logout()
        {
            PublicKey = "";
            Time = "";
        }

        public virtual void RefreshAuthenticationHmac(string time)
        {
            PublicKey = HmacGenerator.PublicKey();
            Time = time;
        }

        public virtual void EncryptPassword()
        {
            Password = Cryptography.Encrypt(Password);
        }

        public virtual void DecryptPassword()
        {
            Password = Cryptography.Decrypt(Password);
        }

        #endregion
    }
}