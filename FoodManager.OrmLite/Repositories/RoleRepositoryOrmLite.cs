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
    public class RoleRepositoryOrmLite : IRoleRepository
    {
        private readonly IDataBaseSqlServerOrmLite _dataBaseSqlServerOrmLite;
        private readonly IAuditEventListener _auditEventListener;

        public RoleRepositoryOrmLite(IDataBaseSqlServerOrmLite dataBaseSqlServerOrmLite, IAuditEventListener auditEventListener)
        {
            _dataBaseSqlServerOrmLite = dataBaseSqlServerOrmLite;
            _auditEventListener = auditEventListener;
        }

        public Role FindBy(int id)
        {
            return _dataBaseSqlServerOrmLite.GetById<Role>(id);
        }

        public IEnumerable<Role> FindBy(Expression<Func<Role, bool>> predicate)
        {
            return _dataBaseSqlServerOrmLite.FindBy(predicate);
        }

        public void Add(Role item)
        {
            _auditEventListener.OnPreInsert(item);
            _dataBaseSqlServerOrmLite.Insert(item);
        }

        public void Update(Role item)
        {
            _auditEventListener.OnPreUpdate(item);
            _dataBaseSqlServerOrmLite.Update(item);
        }

        public void Remove(Role item)
        {
            _auditEventListener.OnPreDelete(item);
            _dataBaseSqlServerOrmLite.LogicRemove(item);
        }

        public bool IsReference(int roleId)
        {
            var amountOfReferences = _dataBaseSqlServerOrmLite.Count<User>(user => user.RoleId == roleId && user.IsActive);
            amountOfReferences += _dataBaseSqlServerOrmLite.Count<Worker>(worker => worker.RoleId == roleId && worker.IsActive);
            amountOfReferences += _dataBaseSqlServerOrmLite.Count<RoleConfiguration>(roleConfiguration => roleConfiguration.RoleId == roleId);
            return amountOfReferences.IsNotZero();
        }

        public IEnumerable<Role> FindAll()
        {
            return _dataBaseSqlServerOrmLite.FindAll<Role>();
        }
    }
}