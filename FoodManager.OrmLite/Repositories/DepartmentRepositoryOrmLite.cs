using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using FoodManager.DataAccess.Listeners;
using FoodManager.Model;
using FoodManager.Model.IRepositories;
using FoodManager.OrmLite.DataBase;

namespace FoodManager.OrmLite.Repositories
{
    public class DepartmentRepositoryOrmLite : IDepartmentRepository
    {
        private readonly IDataBaseSqlServerOrmLite _dataBaseSqlServerOrmLite;
        private readonly IAuditEventListener _auditEventListener;

        public DepartmentRepositoryOrmLite(IDataBaseSqlServerOrmLite dataBaseSqlServerOrmLite, IAuditEventListener auditEventListener)
        {
            _dataBaseSqlServerOrmLite = dataBaseSqlServerOrmLite;
            _auditEventListener = auditEventListener;
        }

        public Department FindBy(int id)
        {
            return _dataBaseSqlServerOrmLite.GetById<Department>(id);
        }

        public IEnumerable<Department> FindBy(Expression<Func<Department, bool>> predicate)
        {
            return _dataBaseSqlServerOrmLite.FindBy(predicate);
        }

        public void Add(Department item)
        {
            _auditEventListener.OnPreInsert(item);
            _dataBaseSqlServerOrmLite.Insert(item);
        }

        public void Update(Department item)
        {
            _auditEventListener.OnPreUpdate(item);
            _dataBaseSqlServerOrmLite.Update(item);
        }

        public void Remove(Department item)
        {
            _dataBaseSqlServerOrmLite.LogicRemoveById<Department>(item.Id);
        }
    }
}