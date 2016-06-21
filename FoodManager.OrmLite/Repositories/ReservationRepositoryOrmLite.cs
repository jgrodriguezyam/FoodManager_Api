using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using FoodManager.DataAccess.Listeners;
using FoodManager.Infrastructure.Integers;
using FoodManager.Model;
using FoodManager.Model.IRepositories;
using FoodManager.OrmLite.DataBase;

namespace FoodManager.OrmLite.Repositories
{
    public class ReservationRepositoryOrmLite : IReservationRepository
    {
        private readonly IDataBaseSqlServerOrmLite _dataBaseSqlServerOrmLite;
        private readonly IAuditEventListener _auditEventListener;

        public ReservationRepositoryOrmLite(IDataBaseSqlServerOrmLite dataBaseSqlServerOrmLite, IAuditEventListener auditEventListener)
        {
            _dataBaseSqlServerOrmLite = dataBaseSqlServerOrmLite;
            _auditEventListener = auditEventListener;
        }

        public Reservation FindBy(int id)
        {
            return _dataBaseSqlServerOrmLite.GetById<Reservation>(id);
        }

        public IEnumerable<Reservation> FindBy(Expression<Func<Reservation, bool>> predicate)
        {
            return _dataBaseSqlServerOrmLite.FindBy(predicate);
        }

        public void Add(Reservation item)
        {
            _auditEventListener.OnPreInsert(item);
            _dataBaseSqlServerOrmLite.Insert(item);
        }

        public void Update(Reservation item)
        {
            _auditEventListener.OnPreUpdate(item);
            _dataBaseSqlServerOrmLite.Update(item);
        }

        public void Remove(Reservation item)
        {
            _auditEventListener.OnPreDelete(item);
            _dataBaseSqlServerOrmLite.LogicRemove(item);
        }

        public bool IsReference(int reservationId)
        {
            var amountOfReferences = _dataBaseSqlServerOrmLite.Count<ReservationDetail>(reservationDetail => reservationDetail.ReservationId == reservationId && reservationDetail.IsActive);
            return amountOfReferences.IsNotZero();
        }
    }
}