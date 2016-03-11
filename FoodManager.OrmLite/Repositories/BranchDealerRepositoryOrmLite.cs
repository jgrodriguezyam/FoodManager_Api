using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using FoodManager.Model;
using FoodManager.Model.IRepositories;
using FoodManager.OrmLite.DataBase;

namespace FoodManager.OrmLite.Repositories
{
    public class BranchDealerRepositoryOrmLite : IBranchDealerRepository
    {
        private readonly IDataBaseSqlServerOrmLite _dataBaseSqlServerOrmLite;

        public BranchDealerRepositoryOrmLite(IDataBaseSqlServerOrmLite dataBaseSqlServerOrmLite)
        {
            _dataBaseSqlServerOrmLite = dataBaseSqlServerOrmLite;
        }

        public BranchDealer FindBy(int id)
        {
            return _dataBaseSqlServerOrmLite.GetById<BranchDealer>(id);
        }

        public IEnumerable<BranchDealer> FindBy(Expression<Func<BranchDealer, bool>> predicate)
        {
            return _dataBaseSqlServerOrmLite.FindBy(predicate);
        }

        public void Add(BranchDealer item)
        {
            _dataBaseSqlServerOrmLite.InsertMiddleEntity(item);
        }

        public void Update(BranchDealer item)
        {
            _dataBaseSqlServerOrmLite.Update(item);
        }

        public void Remove(BranchDealer item)
        {
            _dataBaseSqlServerOrmLite.Remove<BranchDealer>(branchDealer => branchDealer.BranchId == item.BranchId && branchDealer.DealerId == item.DealerId);
        }
    }
}