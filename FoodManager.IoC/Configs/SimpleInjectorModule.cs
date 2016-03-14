using System.Data;
using FoodManager.DataAccess.Listeners;
using FoodManager.Infrastructure.DataBase;
using FoodManager.Infrastructure.Files;
using FoodManager.Model.IHmac;
using FoodManager.Model.IRepositories;
using FoodManager.OrmLite.DataBase;
using FoodManager.OrmLite.Hmac;
using FoodManager.OrmLite.Repositories;
using FoodManager.Queries.Branches;
using FoodManager.Queries.Companies;
using FoodManager.Queries.Dealers;
using FoodManager.Queries.Departments;
using FoodManager.Queries.Diseases;
using FoodManager.Queries.Jobs;
using FoodManager.Queries.Regions;
using FoodManager.Queries.Saucers;
using FoodManager.Queries.Tips;
using FoodManager.Queries.Users;
using FoodManager.Queries.Warnings;
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

            _container.Register<ICompanyRepository, CompanyRepositoryOrmLite>();
            _container.Register<ICompanyQuery, CompanyQuery>();
            _container.Register<ICompanyValidator, CompanyValidator>();
            _container.Register<ICompanyService, CompanyService>();

            _container.Register<IBranchRepository, BranchRepositoryOrmLite>();
            _container.Register<IBranchQuery, BranchQuery>();
            _container.Register<IBranchValidator, BranchValidator>();
            _container.Register<IBranchService, BranchService>();

            _container.Register<IDepartmentRepository, DepartmentRepositoryOrmLite>();
            _container.Register<IDepartmentQuery, DepartmentQuery>();
            _container.Register<IDepartmentValidator, DepartmentValidator>();
            _container.Register<IDepartmentService, DepartmentService>();

            _container.Register<IUserRepository, UserRepositoryOrmLite>();
            _container.Register<IUserQuery, UserQuery>();
            _container.Register<IUserValidator, UserValidator>();
            _container.Register<IUserService, UserService>();

            _container.Register<IDiseaseRepository, DiseaseRepositoryOrmLite>();
            _container.Register<IDiseaseQuery, DiseaseQuery>();
            _container.Register<IDiseaseValidator, DiseaseValidator>();
            _container.Register<IDiseaseService, DiseaseService>();

            _container.Register<IWarningRepository, WarningRepositoryOrmLite>();
            _container.Register<IWarningQuery, WarningQuery>();
            _container.Register<IWarningValidator, WarningValidator>();
            _container.Register<IWarningService, WarningService>();

            _container.Register<IJobRepository, JobRepositoryOrmLite>();
            _container.Register<IJobQuery, JobQuery>();
            _container.Register<IJobValidator, JobValidator>();
            _container.Register<IJobService, JobService>();

            _container.Register<ITipRepository, TipRepositoryOrmLite>();
            _container.Register<ITipQuery, TipQuery>();
            _container.Register<ITipValidator, TipValidator>();
            _container.Register<ITipService, TipService>();

            _container.Register<IDealerRepository, DealerRepositoryOrmLite>();
            _container.Register<IDealerQuery, DealerQuery>();
            _container.Register<IDealerValidator, DealerValidator>();
            _container.Register<IDealerService, DealerService>();

            _container.Register<IBranchDealerRepository, BranchDealerRepositoryOrmLite>();
            _container.Register<IBranchDealerValidator, BranchDealerValidator>();

            _container.Register<ISaucerRepository, SaucerRepositoryOrmLite>();
            _container.Register<ISaucerQuery, SaucerQuery>();
            _container.Register<ISaucerValidator, SaucerValidator>();
            _container.Register<ISaucerService, SaucerService>();

            _container.Register<IDealerSaucerRepository, DealerSaucerRepositoryOrmLite>();
            _container.Register<IDealerSaucerValidator, DealerSaucerValidator>();
        }
    }
}