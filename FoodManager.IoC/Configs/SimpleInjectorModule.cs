using System.Data;
using FoodManager.DataAccess.Listeners;
using FoodManager.Infrastructure.DataBase;
using FoodManager.Infrastructure.Files;
using FoodManager.Model.IHmac;
using FoodManager.Model.IRepositories;
using FoodManager.OrmLite.DataBase;
using FoodManager.OrmLite.Hmac;
using FoodManager.OrmLite.Repositories;
using FoodManager.Queries.AccessLevels;
using FoodManager.Queries.Branches;
using FoodManager.Queries.Companies;
using FoodManager.Queries.Dealers;
using FoodManager.Queries.Departments;
using FoodManager.Queries.Diseases;
using FoodManager.Queries.IngredientGroups;
using FoodManager.Queries.Ingredients;
using FoodManager.Queries.Jobs;
using FoodManager.Queries.Menus;
using FoodManager.Queries.Regions;
using FoodManager.Queries.ReservationDetails;
using FoodManager.Queries.Reservations;
using FoodManager.Queries.RoleConfigurations;
using FoodManager.Queries.Roles;
using FoodManager.Queries.SaucerConfigurations;
using FoodManager.Queries.SaucerMultimedias;
using FoodManager.Queries.Saucers;
using FoodManager.Queries.Tips;
using FoodManager.Queries.Users;
using FoodManager.Queries.Warnings;
using FoodManager.Queries.Workers;
using FoodManager.Services.Factories.Implements;
using FoodManager.Services.Factories.Interfaces;
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
            _container.Register<ICompanyFactory, CompanyFactory>();

            _container.Register<IBranchRepository, BranchRepositoryOrmLite>();
            _container.Register<IBranchQuery, BranchQuery>();
            _container.Register<IBranchValidator, BranchValidator>();
            _container.Register<IBranchService, BranchService>();
            _container.Register<IBranchFactory, BranchFactory>();

            _container.Register<IDepartmentRepository, DepartmentRepositoryOrmLite>();
            _container.Register<IDepartmentQuery, DepartmentQuery>();
            _container.Register<IDepartmentValidator, DepartmentValidator>();
            _container.Register<IDepartmentService, DepartmentService>();
            
            _container.Register<IUserRepository, UserRepositoryOrmLite>();
            _container.Register<IUserQuery, UserQuery>();
            _container.Register<IUserValidator, UserValidator>();
            _container.Register<IUserService, UserService>();
            _container.Register<IUserFactory, UserFactory>();

            _container.Register<IDiseaseRepository, DiseaseRepositoryOrmLite>();
            _container.Register<IDiseaseQuery, DiseaseQuery>();
            _container.Register<IDiseaseValidator, DiseaseValidator>();
            _container.Register<IDiseaseService, DiseaseService>();

            _container.Register<IWarningRepository, WarningRepositoryOrmLite>();
            _container.Register<IWarningQuery, WarningQuery>();
            _container.Register<IWarningValidator, WarningValidator>();
            _container.Register<IWarningService, WarningService>();
            _container.Register<IWarningFactory, WarningFactory>();

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

            _container.Register<ISaucerMultimediaRepository, SaucerMultimediaRepositoryOrmLite>();
            _container.Register<ISaucerMultimediaQuery, SaucerMultimediaQuery>();
            _container.Register<ISaucerMultimediaValidator, SaucerMultimediaValidator>();
            _container.Register<ISaucerMultimediaService, SaucerMultimediaService>();

            _container.Register<IIngredientGroupRepository, IngredientGroupRepositoryOrmLite>();
            _container.Register<IIngredientGroupQuery, IngredientGroupQuery>();
            _container.Register<IIngredientGroupValidator, IngredientGroupValidator>();
            _container.Register<IIngredientGroupService, IngredientGroupService>();
            _container.Register<IIngredientGroupFactory, IngredientGroupFactory>();

            _container.Register<IIngredientRepository, IngredientRepositoryOrmLite>();
            _container.Register<IIngredientQuery, IngredientQuery>();
            _container.Register<IIngredientValidator, IngredientValidator>();
            _container.Register<IIngredientService, IngredientService>();
            _container.Register<IIngredientFactory, IngredientFactory>();

            _container.Register<ISaucerConfigurationRepository, SaucerConfigurationRepositoryOrmLite>();
            _container.Register<ISaucerConfigurationQuery, SaucerConfigurationQuery>();
            _container.Register<ISaucerConfigurationValidator, SaucerConfigurationValidator>();
            _container.Register<ISaucerConfigurationService, SaucerConfigurationService>();
            _container.Register<ISaucerConfigurationFactory, SaucerConfigurationFactory>();

            _container.Register<IWorkerRepository, WorkerRepositoryOrmLite>();
            _container.Register<IWorkerQuery, WorkerQuery>();
            _container.Register<IWorkerValidator, WorkerValidator>();
            _container.Register<IWorkerService, WorkerService>();
            _container.Register<IWorkerFactory, WorkerFactory>();

            _container.Register<IMenuRepository, MenuRepositoryOrmLite>();
            _container.Register<IMenuQuery, MenuQuery>();
            _container.Register<IMenuValidator, MenuValidator>();
            _container.Register<IMenuService, MenuService>();
            _container.Register<IMenuFactory, MenuFactory>();

            _container.Register<IReservationRepository, ReservationRepositoryOrmLite>();
            _container.Register<IReservationQuery, ReservationQuery>();
            _container.Register<IReservationValidator, ReservationValidator>();
            _container.Register<IReservationService, ReservationService>();
            _container.Register<IReservationFactory, ReservationFactory>();

            _container.Register<IRoleRepository, RoleRepositoryOrmLite>();
            _container.Register<IRoleQuery, RoleQuery>();
            _container.Register<IRoleValidator, RoleValidator>();
            _container.Register<IRoleService, RoleService>();

            _container.Register<IRoleConfigurationRepository, RoleConfigurationRepositoryOrmLite>();
            _container.Register<IRoleConfigurationQuery, RoleConfigurationQuery>();
            _container.Register<IRoleConfigurationValidator, RoleConfigurationValidator>();
            _container.Register<IRoleConfigurationService, RoleConfigurationService>();
            _container.Register<IRoleConfigurationFactory, RoleConfigurationFactory>();

            _container.Register<IAccessLevelRepository, AccessLevelRepositoryOrmLite>();
            _container.Register<IAccessLevelQuery, AccessLevelQuery>();

            _container.Register<IPermissionRepository, PermissionRepositoryOrmLite>();
            _container.Register<IPermissionAccessLevelRepository, PermissionAccessLevelRepositoryOrmLite>();
            
            _container.Register<INutritionInformationFactory, NutritionInformationFactory>();

            _container.Register<IReservationDetailRepository, ReservationDetailRepositoryOrmLite>();
            _container.Register<IReservationDetailQuery, ReservationDetailQuery>();
            _container.Register<IReservationDetailService, ReservationDetailService>();
            _container.Register<IReservationDetailFactory, ReservationDetailFactory>();
        }
    }
}