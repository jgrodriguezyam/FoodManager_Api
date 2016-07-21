using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using FoodManager.DataAccess.Listeners;
using FoodManager.Infrastructure.Integers;
using FoodManager.Model;
using FoodManager.Model.IRepositories;
using FoodManager.OrmLite.DataBase;
using ServiceStack.Common.Extensions;

namespace FoodManager.OrmLite.Repositories
{
    public class SaucerRepositoryOrmLite : ISaucerRepository
    {
        private readonly IDataBaseSqlServerOrmLite _dataBaseSqlServerOrmLite;
        private readonly IAuditEventListener _auditEventListener;
        private readonly ISaucerConfigurationRepository _saucerConfigurationRepository;
        private readonly ISaucerMultimediaRepository _saucerMultimediaRepository;

        public SaucerRepositoryOrmLite(IDataBaseSqlServerOrmLite dataBaseSqlServerOrmLite, IAuditEventListener auditEventListener, ISaucerConfigurationRepository saucerConfigurationRepository, ISaucerMultimediaRepository saucerMultimediaRepository)
        {
            _dataBaseSqlServerOrmLite = dataBaseSqlServerOrmLite;
            _auditEventListener = auditEventListener;
            _saucerConfigurationRepository = saucerConfigurationRepository;
            _saucerMultimediaRepository = saucerMultimediaRepository;
        }

        public Saucer FindBy(int id)
        {
            return _dataBaseSqlServerOrmLite.GetById<Saucer>(id);
        }

        public IEnumerable<Saucer> FindBy(Expression<Func<Saucer, bool>> predicate)
        {
            return _dataBaseSqlServerOrmLite.FindBy(predicate);
        }

        public void Add(Saucer item)
        {
            _auditEventListener.OnPreInsert(item);
            _dataBaseSqlServerOrmLite.Insert(item);
        }

        public void Update(Saucer item)
        {
            _auditEventListener.OnPreUpdate(item);
            _dataBaseSqlServerOrmLite.Update(item);
        }

        public void Remove(Saucer item)
        {
            _auditEventListener.OnPreDelete(item);
            _dataBaseSqlServerOrmLite.LogicRemove(item);
            var saucerConfigurations = _saucerConfigurationRepository.FindBy(saucerConfiguration => saucerConfiguration.SaucerId == item.Id && saucerConfiguration.IsActive);
            saucerConfigurations.ForEach(saucerConfiguration => { _saucerConfigurationRepository.Remove(saucerConfiguration); });
            var saucerMultimedias = _saucerMultimediaRepository.FindBy(saucerMultimedia => saucerMultimedia.SaucerId == item.Id && saucerMultimedia.IsActive);
            saucerMultimedias.ForEach(saucerMultimedia => { _saucerMultimediaRepository.Remove(saucerMultimedia); });
        }

        public bool IsReference(int saucerId)
        {
            var amountOfReferences = _dataBaseSqlServerOrmLite.Count<DealerSaucer>(dealerSaucer => dealerSaucer.SaucerId == saucerId);
            amountOfReferences += _dataBaseSqlServerOrmLite.Count<Menu>(menu => menu.SaucerId == saucerId && menu.IsActive);
            amountOfReferences += _dataBaseSqlServerOrmLite.Count<Reservation>(reservation => reservation.SaucerId == saucerId && reservation.IsActive);
            return amountOfReferences.IsNotZero();
        }

        public void AddAll(IEnumerable<Saucer> items)
        {
            items.ForEach(item => { _auditEventListener.OnPreInsert(item); });
            _dataBaseSqlServerOrmLite.InsertAll(items);
        }
    }
}