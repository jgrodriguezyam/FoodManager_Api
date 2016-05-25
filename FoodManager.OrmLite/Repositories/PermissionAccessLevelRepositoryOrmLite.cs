using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using FoodManager.Model;
using FoodManager.Model.IRepositories;
using FoodManager.OrmLite.DataBase;

namespace FoodManager.OrmLite.Repositories
{
    public class PermissionAccessLevelRepositoryOrmLite : IPermissionAccessLevelRepository
    {
        private readonly IDataBaseSqlServerOrmLite _dataBaseSqlServerOrmLite;

        public PermissionAccessLevelRepositoryOrmLite(IDataBaseSqlServerOrmLite dataBaseSqlServerOrmLite)
        {
            _dataBaseSqlServerOrmLite = dataBaseSqlServerOrmLite;
        }

        public PermissionAccessLevel FindBy(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PermissionAccessLevel> FindBy(Expression<Func<PermissionAccessLevel, bool>> predicate)
        {
            return _dataBaseSqlServerOrmLite.FindBy(predicate);
        }

        public void Add(PermissionAccessLevel item)
        {
            throw new NotImplementedException();
        }

        public void Update(PermissionAccessLevel item)
        {
            throw new NotImplementedException();
        }

        public void Remove(PermissionAccessLevel item)
        {
            throw new NotImplementedException();
        }
    }
}