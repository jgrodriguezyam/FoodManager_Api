using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using FoodManager.DataAccess.Listeners;
using FoodManager.Model;
using FoodManager.Model.IRepositories;
using FoodManager.OrmLite.DataBase;

namespace FoodManager.OrmLite.Repositories
{
    public class ReservationDetailRepositoryOrmLite : IReservationDetailRepository
    {
        private readonly IDataBaseSqlServerOrmLite _dataBaseSqlServerOrmLite;
        private readonly IAuditEventListener _auditEventListener;

        public ReservationDetailRepositoryOrmLite(IDataBaseSqlServerOrmLite dataBaseSqlServerOrmLite, IAuditEventListener auditEventListener)
        {
            _dataBaseSqlServerOrmLite = dataBaseSqlServerOrmLite;
            _auditEventListener = auditEventListener;
        }

        public ReservationDetail FindBy(int id)
        {
            return _dataBaseSqlServerOrmLite.GetById<ReservationDetail>(id);
        }

        public IEnumerable<ReservationDetail> FindBy(Expression<Func<ReservationDetail, bool>> predicate)
        {
            return _dataBaseSqlServerOrmLite.FindBy(predicate);
        }

        public void Add(ReservationDetail item)
        {
            _auditEventListener.OnPreInsert(item);
            _dataBaseSqlServerOrmLite.Insert(item);
        }

        public void Update(ReservationDetail item)
        {
            throw new NotImplementedException();
        }

        public void Remove(ReservationDetail item)
        {
            _auditEventListener.OnPreDelete(item);
            _dataBaseSqlServerOrmLite.LogicRemove(item);
        }
    }
}