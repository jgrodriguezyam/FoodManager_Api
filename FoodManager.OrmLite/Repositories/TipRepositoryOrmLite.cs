using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using FoodManager.DataAccess.Listeners;
using FoodManager.Model;
using FoodManager.Model.IRepositories;
using FoodManager.OrmLite.DataBase;
using ServiceStack.Common.Extensions;

namespace FoodManager.OrmLite.Repositories
{
    public class TipRepositoryOrmLite : ITipRepository
    {
        private readonly IDataBaseSqlServerOrmLite _dataBaseSqlServerOrmLite;
        private readonly IAuditEventListener _auditEventListener;

        public TipRepositoryOrmLite(IDataBaseSqlServerOrmLite dataBaseSqlServerOrmLite, IAuditEventListener auditEventListener)
        {
            _dataBaseSqlServerOrmLite = dataBaseSqlServerOrmLite;
            _auditEventListener = auditEventListener;
        }

        public Tip FindBy(int id)
        {
            return _dataBaseSqlServerOrmLite.GetById<Tip>(id);
        }

        public IEnumerable<Tip> FindBy(Expression<Func<Tip, bool>> predicate)
        {
            return _dataBaseSqlServerOrmLite.FindBy(predicate);
        }

        public void Add(Tip item)
        {
            _auditEventListener.OnPreInsert(item);
            _dataBaseSqlServerOrmLite.Insert(item);
        }

        public void Update(Tip item)
        {
            _auditEventListener.OnPreUpdate(item);
            _dataBaseSqlServerOrmLite.Update(item);
        }

        public void Remove(Tip item)
        {
            _auditEventListener.OnPreDelete(item);
            _dataBaseSqlServerOrmLite.LogicRemove(item);
        }

        public void AddAll(IEnumerable<Tip> items)
        {
            items.ForEach(item => { _auditEventListener.OnPreInsert(item); });
            _dataBaseSqlServerOrmLite.InsertAll(items);
        }
    }
}