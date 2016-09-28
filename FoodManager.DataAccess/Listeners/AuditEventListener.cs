using System;
using FoodManager.Infrastructure.Constants;
using FoodManager.Infrastructure.Dates;
using FoodManager.Infrastructure.Enums;
using FoodManager.Infrastructure.Http;
using FoodManager.Infrastructure.Validators.Enums;
using FoodManager.Model.Base;
using FoodManager.Model.Sessions;

namespace FoodManager.DataAccess.Listeners
{
    public class AuditEventListener : IAuditEventListener
    {
        private readonly IUserSession _userSession;
        private readonly IWorkerSession _workerSession;

        public AuditEventListener(IUserSession userSession, IWorkerSession workerSession)
        {
            _userSession = userSession;
            _workerSession = workerSession;
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

        public void OnPreInsertForSystem(object entity)
        {
            if (entity is IAuditInfo)
                SetAuditForSystem(entity, EventType.Create);
        }

        public void OnPreUpdateForSystem(object entity)
        {
            if (entity is IAuditInfo)
                SetAuditForSystem(entity, EventType.Update);
        }

        private void SetAudit(object entity, EventType eventType)
        {
            var publicKey = HttpContextAccess.GetPublicKey();
            var loginType = HttpContextAccess.GetLoginType();

            var id = GlobalConstants.SystemUserId;
            if (loginType.Value == LoginType.User.GetValue())
                id = _userSession.FindUserByPublicKey(publicKey).Id;

            if (loginType.Value == LoginType.Worker.GetValue())
                id = _workerSession.FindWorkerByPublicKey(publicKey).Id;

            //const int id = 1;
            SetAuditToEntity(entity, eventType, id);
        }

        private void SetAuditForSystem(object entity, EventType eventType)
        {
            var id = GlobalConstants.SystemUserId;
            SetAuditToEntity(entity, eventType, id);
        }

        private void SetAuditToEntity(object entity, EventType eventType, int id)
        {
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