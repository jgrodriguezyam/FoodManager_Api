using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using FoodManager.Model;
using FoodManager.Model.IRepositories;
using FoodManager.OrmLite.DataBase;

namespace FoodManager.OrmLite.Repositories
{
    public class DealerSaucerRepositoryOrmLite : IDealerSaucerRepository
    {
        private readonly IDataBaseSqlServerOrmLite _dataBaseSqlServerOrmLite;

        public DealerSaucerRepositoryOrmLite(IDataBaseSqlServerOrmLite dataBaseSqlServerOrmLite)
        {
            _dataBaseSqlServerOrmLite = dataBaseSqlServerOrmLite;
        }

        public DealerSaucer FindBy(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DealerSaucer> FindBy(Expression<Func<DealerSaucer, bool>> predicate)
        {
            return _dataBaseSqlServerOrmLite.FindBy(predicate);
        }

        public void Add(DealerSaucer item)
        {
            _dataBaseSqlServerOrmLite.InsertMiddleEntity(item);
        }

        public void Update(DealerSaucer item)
        {
            _dataBaseSqlServerOrmLite.Update(item);
        }

        public void Remove(DealerSaucer item)
        {
            _dataBaseSqlServerOrmLite.RemoveMiddleEntity(item);
        }
    }
}