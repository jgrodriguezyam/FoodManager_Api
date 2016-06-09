using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using FoodManager.Model;
using FoodManager.Model.IRepositories;
using FoodManager.OrmLite.DataBase;

namespace FoodManager.OrmLite.Repositories
{
    public class PermissionRepositoryOrmLite : IPermissionRepository
    {
        private readonly IDataBaseSqlServerOrmLite _dataBaseSqlServerOrmLite;

        public PermissionRepositoryOrmLite(IDataBaseSqlServerOrmLite dataBaseSqlServerOrmLite)
        {
            _dataBaseSqlServerOrmLite = dataBaseSqlServerOrmLite;
        }

        public Permission FindBy(int id)
        {
            return _dataBaseSqlServerOrmLite.GetByIdOrDefault<Permission>(id);
        }

        public IEnumerable<Permission> FindBy(Expression<Func<Permission, bool>> predicate)
        {
            return _dataBaseSqlServerOrmLite.FindBy(predicate);
        }

        public void Add(Permission item)
        {
            throw new NotImplementedException();
        }

        public void Update(Permission item)
        {
            throw new NotImplementedException();
        }

        public void Remove(Permission item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Permission> FindAll()
        {
            return _dataBaseSqlServerOrmLite.FindAll<Permission>();
        }
    }
}