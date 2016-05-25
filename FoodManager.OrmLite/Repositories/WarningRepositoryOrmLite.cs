using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using FoodManager.DataAccess.Listeners;
using FoodManager.Model;
using FoodManager.Model.IRepositories;
using FoodManager.OrmLite.DataBase;

namespace FoodManager.OrmLite.Repositories
{
    public class WarningRepositoryOrmLite : IWarningRepository
    {
        private readonly IDataBaseSqlServerOrmLite _dataBaseSqlServerOrmLite;
        private readonly IAuditEventListener _auditEventListener;

        public WarningRepositoryOrmLite(IDataBaseSqlServerOrmLite dataBaseSqlServerOrmLite, IAuditEventListener auditEventListener)
        {
            _dataBaseSqlServerOrmLite = dataBaseSqlServerOrmLite;
            _auditEventListener = auditEventListener;
        }

        public Warning FindBy(int id)
        {
            return _dataBaseSqlServerOrmLite.GetById<Warning>(id);
        }

        public IEnumerable<Warning> FindBy(Expression<Func<Warning, bool>> predicate)
        {
            return _dataBaseSqlServerOrmLite.FindBy(predicate);
        }

        public void Add(Warning item)
        {
            _auditEventListener.OnPreInsert(item);
            _dataBaseSqlServerOrmLite.Insert(item);
        }

        public void Update(Warning item)
        {
            _auditEventListener.OnPreUpdate(item);
            _dataBaseSqlServerOrmLite.Update(item);
        }

        public void Remove(Warning item)
        {
            _auditEventListener.OnPreDelete(item);
            _dataBaseSqlServerOrmLite.LogicRemove(item);
        }
    }
}