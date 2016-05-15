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
    public class DealerRepositoryOrmLite : IDealerRepository
    {
        private readonly IDataBaseSqlServerOrmLite _dataBaseSqlServerOrmLite;
        private readonly IAuditEventListener _auditEventListener;

        public DealerRepositoryOrmLite(IDataBaseSqlServerOrmLite dataBaseSqlServerOrmLite, IAuditEventListener auditEventListener)
        {
            _dataBaseSqlServerOrmLite = dataBaseSqlServerOrmLite;
            _auditEventListener = auditEventListener;
        }

        public Dealer FindBy(int id)
        {
            return _dataBaseSqlServerOrmLite.GetById<Dealer>(id);
        }

        public IEnumerable<Dealer> FindBy(Expression<Func<Dealer, bool>> predicate)
        {
            return _dataBaseSqlServerOrmLite.FindBy(predicate);
        }

        public void Add(Dealer item)
        {
            _auditEventListener.OnPreInsert(item);
            _dataBaseSqlServerOrmLite.Insert(item);
        }

        public void Update(Dealer item)
        {
            _auditEventListener.OnPreUpdate(item);
            _dataBaseSqlServerOrmLite.Update(item);
        }

        public void Remove(Dealer item)
        {
            _auditEventListener.OnPreDelete(item);
            _dataBaseSqlServerOrmLite.LogicRemove(item);
        }

        public bool IsReference(int dealerId)
        {
            var amountOfReferences = _dataBaseSqlServerOrmLite.Count<BranchDealer>(branchDealer => branchDealer.DealerId == dealerId);
            amountOfReferences += _dataBaseSqlServerOrmLite.Count<User>(user => user.DealerId == dealerId && user.IsActive);
            amountOfReferences += _dataBaseSqlServerOrmLite.Count<DealerSaucer>(dealerSaucer => dealerSaucer.DealerId == dealerId);
            amountOfReferences += _dataBaseSqlServerOrmLite.Count<Menu>(menu => menu.DealerId == dealerId && menu.IsActive);
            amountOfReferences += _dataBaseSqlServerOrmLite.Count<Reservation>(reservation => reservation.DealerId == dealerId && reservation.IsActive);
            return amountOfReferences.IsNotZero();
        }
    }
}