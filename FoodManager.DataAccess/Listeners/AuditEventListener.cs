using System;
using System.Web;
using FoodManager.Infrastructure.Constants;
using FoodManager.Infrastructure.Dates;
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

        private void SetAudit(object entity, EventType eventType)
        {
            //var headerPublicKey = HttpContext.Current.Request.Headers[GlobalConstants.PublicKey];
            //var user = _hmacHelper.FindUserByPublicKey(headerPublicKey);
            //var userId = user.Id;
            var userId = 1;

            var entityToAudit = entity as IAuditInfo;
            var today = DateTime.Now.ToDateTimeString().DateTimeStringToDateTime();

            switch (eventType)
            {
                case EventType.Create:
                    entityToAudit.CreatedOn = today;
                    entityToAudit.ModifiedOn = today;
                    entityToAudit.CreatedBy = userId;
                    entityToAudit.ModifiedBy = userId;
                    entityToAudit.Status = true;
                    if (entity is IDeletable)
                    {
                        var entityDeletable = entity as IDeletable;
                        entityDeletable.IsActive = true;
                    }
                break;
                case EventType.Update:
                    entityToAudit.ModifiedBy = userId;
                    entityToAudit.ModifiedOn = today;
                    break;
            }
        }
    }
}