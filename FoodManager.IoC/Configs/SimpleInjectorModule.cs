﻿using System.Data;
using FoodManager.DataAccess.Listeners;
using FoodManager.Infrastructure.DataBase;
using FoodManager.Infrastructure.Files;
using FoodManager.Model.IHmac;
using FoodManager.Model.IRepositories;
using FoodManager.OrmLite.DataBase;
using FoodManager.OrmLite.Hmac;
using FoodManager.OrmLite.Repositories;
using FoodManager.Queries.Regions;
using FoodManager.Services.Implements;
using FoodManager.Services.Interfaces;
using FoodManager.Services.Validators.Implements;
using FoodManager.Services.Validators.Interfaces;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.SqlServer;
using SimpleInjector;

namespace FoodManager.IoC.Configs
{
    public static class SimpleInjectorModule
    {
        private static Container _container;

        public static void SetContainer(Container container)
        {
            _container = container;
        }

        public static Container GetContainer()
        {
            return _container;
        }

        public static void VerifyContainer()
        {
            _container.Verify();
        }

        public static void Load()
        {
            _container.RegisterWebApiRequest<IDbConnectionFactory>(() => new OrmLiteConnectionFactory(ConnectionSettings.ConnectionString(), new SqlServerOrmLiteDialectProvider()));
            _container.RegisterWebApiRequest<IDataBaseSqlServerOrmLite, DataBaseSqlServerOrmLite>();
            _container.RegisterInitializer<DataBaseSqlServerOrmLite>(handler =>
            {
                handler.DbConnectionFactory = _container.GetInstance<IDbConnectionFactory>();
                handler.DbConnection = handler.DbConnectionFactory.OpenDbConnection();
                handler.DbTransaction = handler.DbConnection.BeginTransaction(IsolationLevel.Snapshot);
            });
            _container.Register<IAuditEventListener, AuditEventListener>();
            _container.Register<IFileResolver, FileResolver>();
            _container.Register<IFileValidator, FileValidator>();
            _container.Register<IStorageProvider, StorageProvider>();
            _container.Register<IHmacHelper, HmacHelperOrmLite>();

            _container.Register<IRegionRepository, RegionRepositoryOrmLite>();
            _container.Register<IRegionQuery, RegionQuery>();
            _container.Register<IRegionValidator, RegionValidator>();
            _container.Register<IRegionService, RegionService>();
        }
    }
}