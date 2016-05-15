using FoodManager.Infrastructure.Hmac;
using FoodManager.Model.Base;
using ServiceStack.DataAnnotations;

namespace FoodManager.Model
{
    public class Worker : EntityBase, IDeletable
    {
        [AutoIncrement]
        public int Id { get; set; }
        public string Code { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Imss { get; set; }
        public int Gender { get; set; }
        public string Badge { get; set; }
        public string PublicKey { get; set; }
        public string Time { get; set; }
        public int DepartmentId { get; set; }
        public int JobId { get; set; }
        public int DealerId { get; set; }
        public int RoleId { get; set; }
        public int BranchId { get; set; }

        public bool IsActive { get; set; }

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

        #endregion
    }
}