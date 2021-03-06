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
    public class SaucerRepositoryOrmLite : ISaucerRepository
    {
        private readonly IDataBaseSqlServerOrmLite _dataBaseSqlServerOrmLite;
        private readonly IAuditEventListener _auditEventListener;

        public SaucerRepositoryOrmLite(IDataBaseSqlServerOrmLite dataBaseSqlServerOrmLite, IAuditEventListener auditEventListener)
        {
            _dataBaseSqlServerOrmLite = dataBaseSqlServerOrmLite;
            _auditEventListener = auditEventListener;
        }

        public Saucer FindBy(int id)
        {
            return _dataBaseSqlServerOrmLite.GetById<Saucer>(id);
        }

        public IEnumerable<Saucer> FindBy(Expression<Func<Saucer, bool>> predicate)
        {
            return _dataBaseSqlServerOrmLite.FindBy(predicate);
        }

        public void Add(Saucer item)
        {
            _auditEventListener.OnPreInsert(item);
            _dataBaseSqlServerOrmLite.Insert(item);
        }

        public void Update(Saucer item)
        {
            _auditEventListener.OnPreUpdate(item);
            _dataBaseSqlServerOrmLite.Update(item);
        }

        public void Remove(Saucer item)
        {
            _auditEventListener.OnPreDelete(item);
            _dataBaseSqlServerOrmLite.LogicRemove(item);
        }

        public bool IsReference(int saucerId)
        {
            var amountOfReferences = _dataBaseSqlServerOrmLite.Count<SaucerConfiguration>(saucerConfiguration => saucerConfiguration.SaucerId == saucerId && saucerConfiguration.IsActive);
            amountOfReferences += _dataBaseSqlServerOrmLite.Count<SaucerMultimedia>(saucerMultimedia => saucerMultimedia.SaucerId == saucerId && saucerMultimedia.IsActive);
            amountOfReferences += _dataBaseSqlServerOrmLite.Count<DealerSaucer>(dealerSaucer => dealerSaucer.SaucerId == saucerId);
            amountOfReferences += _dataBaseSqlServerOrmLite.Count<Menu>(menu => menu.SaucerId == saucerId && menu.IsActive);
            amountOfReferences += _dataBaseSqlServerOrmLite.Count<Reservation>(reservation => reservation.SaucerId == saucerId && reservation.IsActive);
            return amountOfReferences.IsNotZero();
        }
    }
}