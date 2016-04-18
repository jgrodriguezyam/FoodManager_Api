using System;
using System.Linq;
using System.Web;
using FoodManager.Infrastructure.Constants;
using FoodManager.Infrastructure.Dates;
using FoodManager.Infrastructure.Enums;
using FoodManager.Infrastructure.Validators.Enums;
using FoodManager.Model.Base;
using FoodManager.Model.IHmac;

namespace FoodManager.DataAccess.Listeners
{
    public class AuditEventListener : IAuditEventListener
    {
        private readonly IHmacHelper _hmacHelper;

        public AuditEventListener(IHmacHelper hmacHelper)
        {
            _hmacHelper = hmacHelper;
        }

        public void OnPreInsert(object entity)
        {
            if (entity is IAuditInfo)
                SetAudit(entity, EventType.Create);
        }

        public void OnPreUpdate(object entity)
        {
            if (entity is IAuditInfo)
                SetAudit(entity, EventType.Update);
        }

        public void OnPreDelete(object entity)
        {
            if (entity is IAuditInfo)
                SetAudit(entity, EventType.Delete);
        }

        private void SetAudit(object entity, EventType eventType)
        {
            var headerPublicKey = HttpContext.Current.Request.Headers[GlobalConstants.PublicKey];
            var headerLoginType = HttpContext.Current.Request.Headers[GlobalConstants.LoginType];
            var loginType = new LoginType().ConvertToCollection().FirstOrDefault(loginTp => loginTp.Value == int.Parse(headerLoginType));

            var id = GlobalConstants.SystemUserId;
            if (loginType.Value == LoginType.User.GetValue())
                id = _hmacHelper.FindUserByPublicKey(headerPublicKey).Id;

            if (loginType.Value == LoginType.Worker.GetValue())
                id = _hmacHelper.FindWorkerByPublicKey(headerPublicKey).Id;

            //const int userId = 1;
            var entityToAudit = entity as IAuditInfo;
            var today = DateTime.Now.ToDateTimeString().DateTimeStringToDateTime();

            switch (eventType)
            {
                case EventType.Create:
                    entityToAudit.CreatedOn = today;
                    entityToAudit.ModifiedOn = today;
                    entityToAudit.CreatedBy = id;
                    entityToAudit.ModifiedBy = id;
                    entityToAudit.Status = GlobalConstants.StatusActivated;
                    if (entity is IDeletable)
                    {
                        var entityDeletable = entity as IDeletable;
                        entityDeletable.IsActive = GlobalConstants.Activated;
                    }
                    break;
                case EventType.Update:
                    entityToAudit.ModifiedBy = id;
                    entityToAudit.ModifiedOn = today;
                    break;
                case EventType.Delete:
                    entityToAudit.ModifiedBy = id;
                    entityToAudit.ModifiedOn = today;
                    entityToAudit.Status = GlobalConstants.StatusDeactivated;
                    if (entity is IDeletable)
                    {
                        var entityDeletable = entity as IDeletable;
                        entityDeletable.IsActive = GlobalConstants.Deactivated;
                    }
                    break;
            }
        }
    }
}