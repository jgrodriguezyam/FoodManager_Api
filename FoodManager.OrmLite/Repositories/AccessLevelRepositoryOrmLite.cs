using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using FoodManager.Model;
using FoodManager.Model.IRepositories;
using FoodManager.OrmLite.DataBase;

namespace FoodManager.OrmLite.Repositories
{
    public class AccessLevelRepositoryOrmLite : IAccessLevelRepository
    {
        private readonly IDataBaseSqlServerOrmLite _dataBaseSqlServerOrmLite;

        public AccessLevelRepositoryOrmLite(IDataBaseSqlServerOrmLite dataBaseSqlServerOrmLite)
        {
            _dataBaseSqlServerOrmLite = dataBaseSqlServerOrmLite;
        }

        public AccessLevel FindBy(int id)
        {
            return _dataBaseSqlServerOrmLite.GetByIdOrDefault<AccessLevel>(id);
        }

        public IEnumerable<AccessLevel> FindBy(Expression<Func<AccessLevel, bool>> predicate)
        {
            return _dataBaseSqlServerOrmLite.FindBy(predicate);
        }

        public void Add(AccessLevel item)
        {
            throw new NotImplementedException();
        }

        public void Update(AccessLevel item)
        {
            throw new NotImplementedException();
        }

        public void Remove(AccessLevel item)
        {
            throw new NotImplementedException();
        }
    }
}