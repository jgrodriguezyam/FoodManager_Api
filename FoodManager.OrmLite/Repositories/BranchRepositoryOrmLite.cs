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
    public class BranchRepositoryOrmLite : IBranchRepository
    {
        private readonly IDataBaseSqlServerOrmLite _dataBaseSqlServerOrmLite;
        private readonly IAuditEventListener _auditEventListener;

        public BranchRepositoryOrmLite(IDataBaseSqlServerOrmLite dataBaseSqlServerOrmLite, IAuditEventListener auditEventListener)
        {
            _dataBaseSqlServerOrmLite = dataBaseSqlServerOrmLite;
            _auditEventListener = auditEventListener;
        }

        public Branch FindBy(int id)
        {
            return _dataBaseSqlServerOrmLite.GetById<Branch>(id);
        }

        public IEnumerable<Branch> FindBy(Expression<Func<Branch, bool>> predicate)
        {
            return _dataBaseSqlServerOrmLite.FindBy(predicate);
        }

        public void Add(Branch item)
        {
            _auditEventListener.OnPreInsert(item);
            _dataBaseSqlServerOrmLite.Insert(item);
        }

        public void Update(Branch item)
        {
            _auditEventListener.OnPreUpdate(item);
            _dataBaseSqlServerOrmLite.Update(item);
        }

        public void Remove(Branch item)
        {
            _auditEventListener.OnPreDelete(item);
            _dataBaseSqlServerOrmLite.LogicRemove(item);
        }

        public bool IsReference(int branchId)
        {
            var amountOfReferences = _dataBaseSqlServerOrmLite.Count<BranchDealer>(branchDealer => branchDealer.BranchId == branchId);
            return amountOfReferences.IsNotZero();
        }
    }
}