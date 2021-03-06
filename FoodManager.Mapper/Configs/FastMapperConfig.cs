using FastMapper;
using FoodManager.DTO.BaseRequest;
using FoodManager.DTO.Message.AccessLevels;
using FoodManager.DTO.Message.Branches;
using FoodManager.DTO.Message.Companies;
using FoodManager.DTO.Message.Dealers;
using FoodManager.DTO.Message.Departments;
using FoodManager.DTO.Message.Diseases;
using FoodManager.DTO.Message.IngredientGroups;
using FoodManager.DTO.Message.Ingredients;
using FoodManager.DTO.Message.Jobs;
using FoodManager.DTO.Message.Menus;
using FoodManager.DTO.Message.Regions;
using FoodManager.DTO.Message.Reservations;
using FoodManager.DTO.Message.RoleConfigurations;
using FoodManager.DTO.Message.Roles;
using FoodManager.DTO.Message.SaucerConfigurations;
using FoodManager.DTO.Message.SaucerMultimedias;
using FoodManager.DTO.Message.Saucers;
using FoodManager.DTO.Message.Tips;
using FoodManager.DTO.Message.Users;
using FoodManager.DTO.Message.Warnings;
using FoodManager.DTO.Message.Workers;
using FoodManager.Infrastructure.Dates;
using FoodManager.Mapper.Resolvers;

namespace FoodManager.Mapper.Configs
{
    public static class FastMapperConfig
    {
        public static void Initialize()
        {
            #region Region

            TypeAdapterConfig<Model.Region, DTO.Region>
                .NewConfig();

            TypeAdapterConfig<DTO.Region, Model.Region>
                .NewConfig()
                .IgnoreMember(dest => dest.CreatedBy)
                .IgnoreMember(dest => dest.CreatedOn)
                .IgnoreMember(dest => dest.ModifiedBy)
                .IgnoreMember(dest => dest.ModifiedOn)
                .IgnoreMember(dest => dest.IsActive)
                .IgnoreMember(dest => dest.Status);

            TypeAdapterConfig<Model.Region, Model.Region>
                .NewConfig()
                .IgnoreMember(dest => dest.Id)
                .IgnoreMember(dest => dest.CreatedBy)
                .IgnoreMember(dest => dest.CreatedOn)
                .IgnoreMember(dest => dest.ModifiedBy)
                .IgnoreMember(dest => dest.ModifiedOn)
                .IgnoreMember(dest => dest.IsActive)
                .IgnoreMember(dest => dest.Status);

            TypeAdapterConfig<RegionRequest, Model.Region>
                .NewConfig();

            TypeAdapterConfig<Model.Region, RegionResponse>
                .NewConfig();

            #endregion

            #region Company

            TypeAdapterConfig<Model.Company, DTO.Company>
                .NewConfig();

            TypeAdapterConfig<DTO.Company, Model.Company>
                .NewConfig()
                .IgnoreMember(dest => dest.CreatedBy)
                .IgnoreMember(dest => dest.CreatedOn)
                .IgnoreMember(dest => dest.ModifiedBy)
                .IgnoreMember(dest => dest.ModifiedOn)
                .IgnoreMember(dest => dest.IsActive)
                .IgnoreMember(dest => dest.Status);

            TypeAdapterConfig<Model.Company, Model.Company>
                .NewConfig()
                .IgnoreMember(dest => dest.Id)
                .IgnoreMember(dest => dest.CreatedBy)
                .IgnoreMember(dest => dest.CreatedOn)
                .IgnoreMember(dest => dest.ModifiedBy)
                .IgnoreMember(dest => dest.ModifiedOn)
                .IgnoreMember(dest => dest.IsActive)
                .IgnoreMember(dest => dest.Status);

            TypeAdapterConfig<CompanyRequest, Model.Company>
                .NewConfig();

            TypeAdapterConfig<Model.Company, CompanyResponse>
                .NewConfig();

            #endregion

            #region Branch

            TypeAdapterConfig<Model.Branch, DTO.Branch>
                .NewConfig();

            TypeAdapterConfig<DTO.Branch, Model.Branch>
                .NewConfig()
                .IgnoreMember(dest => dest.CreatedBy)
                .IgnoreMember(dest => dest.CreatedOn)
                .IgnoreMember(dest => dest.ModifiedBy)
                .IgnoreMember(dest => dest.ModifiedOn)
                .IgnoreMember(dest => dest.IsActive)
                .IgnoreMember(dest => dest.Status);

            TypeAdapterConfig<Model.Branch, Model.Branch>
                .NewConfig()
                .IgnoreMember(dest => dest.Id)
                .IgnoreMember(dest => dest.CreatedBy)
                .IgnoreMember(dest => dest.CreatedOn)
                .IgnoreMember(dest => dest.ModifiedBy)
                .IgnoreMember(dest => dest.ModifiedOn)
                .IgnoreMember(dest => dest.IsActive)
                .IgnoreMember(dest => dest.Status);

            TypeAdapterConfig<BranchRequest, Model.Branch>
                .NewConfig();

            TypeAdapterConfig<Model.Branch, BranchResponse>
                .NewConfig();

            #endregion

            #region Department

            TypeAdapterConfig<Model.Department, DTO.Department>
                .NewConfig();

            TypeAdapterConfig<DTO.Department, Model.Department>
                .NewConfig()
                .IgnoreMember(dest => dest.CreatedBy)
                .IgnoreMember(dest => dest.CreatedOn)
                .IgnoreMember(dest => dest.ModifiedBy)
                .IgnoreMember(dest => dest.ModifiedOn)
                .IgnoreMember(dest => dest.IsActive)
                .IgnoreMember(dest => dest.Status);

            TypeAdapterConfig<Model.Department, Model.Department>
                .NewConfig()
                .IgnoreMember(dest => dest.Id)
                .IgnoreMember(dest => dest.CreatedBy)
                .IgnoreMember(dest => dest.CreatedOn)
                .IgnoreMember(dest => dest.ModifiedBy)
                .IgnoreMember(dest => dest.ModifiedOn)
                .IgnoreMember(dest => dest.IsActive)
                .IgnoreMember(dest => dest.Status);

            TypeAdapterConfig<DepartmentRequest, Model.Department>
                .NewConfig();

            TypeAdapterConfig<Model.Department, DepartmentResponse>
                .NewConfig();

            #endregion

            #region User

            TypeAdapterConfig<Model.User, DTO.User>
                .NewConfig();

            TypeAdapterConfig<DTO.User, Model.User>
                .NewConfig()
                .IgnoreMember(dest => dest.CreatedBy)
                .IgnoreMember(dest => dest.CreatedOn)
                .IgnoreMember(dest => dest.ModifiedBy)
                .IgnoreMember(dest => dest.ModifiedOn)
                .IgnoreMember(dest => dest.IsActive)
                .IgnoreMember(dest => dest.Status);

            TypeAdapterConfig<Model.User, Model.User>
                .NewConfig()
                .IgnoreMember(dest => dest.Id)
                .IgnoreMember(dest => dest.CreatedBy)
                .IgnoreMember(dest => dest.CreatedOn)
                .IgnoreMember(dest => dest.ModifiedBy)
                .IgnoreMember(dest => dest.ModifiedOn)
                .IgnoreMember(dest => dest.IsActive)
                .IgnoreMember(dest => dest.Status)
                .IgnoreMember(dest => dest.UserName)
                .IgnoreMember(dest => dest.Password)
                .IgnoreMember(dest => dest.PublicKey)
                .IgnoreMember(dest => dest.Time);

            TypeAdapterConfig<UserRequest, Model.User>
                .NewConfig();

            TypeAdapterConfig<Model.User, UserResponse>
                .NewConfig();

            #endregion

            #region Disease

            TypeAdapterConfig<Model.Disease, DTO.Disease>
                .NewConfig();

            TypeAdapterConfig<DTO.Disease, Model.Disease>
                .NewConfig()
                .IgnoreMember(dest => dest.CreatedBy)
                .IgnoreMember(dest => dest.CreatedOn)
                .IgnoreMember(dest => dest.ModifiedBy)
                .IgnoreMember(dest => dest.ModifiedOn)
                .IgnoreMember(dest => dest.IsActive)
                .IgnoreMember(dest => dest.Status);

            TypeAdapterConfig<Model.Disease, Model.Disease>
                .NewConfig()
                .IgnoreMember(dest => dest.Id)
                .IgnoreMember(dest => dest.CreatedBy)
                .IgnoreMember(dest => dest.CreatedOn)
                .IgnoreMember(dest => dest.ModifiedBy)
                .IgnoreMember(dest => dest.ModifiedOn)
                .IgnoreMember(dest => dest.IsActive)
                .IgnoreMember(dest => dest.Status);

            TypeAdapterConfig<DiseaseRequest, Model.Disease>
                .NewConfig();

            TypeAdapterConfig<Model.Disease, DiseaseResponse>
                .NewConfig();

            #endregion

            #region Warning

            TypeAdapterConfig<Model.Warning, DTO.Warning>
                .NewConfig();

            TypeAdapterConfig<DTO.Warning, Model.Warning>
                .NewConfig()
                .IgnoreMember(dest => dest.CreatedBy)
                .IgnoreMember(dest => dest.CreatedOn)
                .IgnoreMember(dest => dest.ModifiedBy)
                .IgnoreMember(dest => dest.ModifiedOn)
                .IgnoreMember(dest => dest.IsActive)
                .IgnoreMember(dest => dest.Status);

            TypeAdapterConfig<Model.Warning, Model.Warning>
                .NewConfig()
                .IgnoreMember(dest => dest.Id)
                .IgnoreMember(dest => dest.CreatedBy)
                .IgnoreMember(dest => dest.CreatedOn)
                .IgnoreMember(dest => dest.ModifiedBy)
                .IgnoreMember(dest => dest.ModifiedOn)
                .IgnoreMember(dest => dest.IsActive)
                .IgnoreMember(dest => dest.Status);

            TypeAdapterConfig<WarningRequest, Model.Warning>
                .NewConfig();

            TypeAdapterConfig<Model.Warning, WarningResponse>
                .NewConfig();

            #endregion

            #region Job

            TypeAdapterConfig<Model.Job, DTO.Job>
                .NewConfig();

            TypeAdapterConfig<DTO.Job, Model.Job>
                .NewConfig()
                .IgnoreMember(dest => dest.CreatedBy)
                .IgnoreMember(dest => dest.CreatedOn)
                .IgnoreMember(dest => dest.ModifiedBy)
                .IgnoreMember(dest => dest.ModifiedOn)
                .IgnoreMember(dest => dest.IsActive)
                .IgnoreMember(dest => dest.Status);

            TypeAdapterConfig<Model.Job, Model.Job>
                .NewConfig()
                .IgnoreMember(dest => dest.Id)
                .IgnoreMember(dest => dest.CreatedBy)
                .IgnoreMember(dest => dest.CreatedOn)
                .IgnoreMember(dest => dest.ModifiedBy)
                .IgnoreMember(dest => dest.ModifiedOn)
                .IgnoreMember(dest => dest.IsActive)
                .IgnoreMember(dest => dest.Status);

            TypeAdapterConfig<JobRequest, Model.Job>
                .NewConfig();

            TypeAdapterConfig<Model.Job, JobResponse>
                .NewConfig();

            #endregion

            #region Tip

            TypeAdapterConfig<Model.Tip, DTO.Tip>
                .NewConfig();

            TypeAdapterConfig<DTO.Tip, Model.Tip>
                .NewConfig()
                .IgnoreMember(dest => dest.CreatedBy)
                .IgnoreMember(dest => dest.CreatedOn)
                .IgnoreMember(dest => dest.ModifiedBy)
                .IgnoreMember(dest => dest.ModifiedOn)
                .IgnoreMember(dest => dest.IsActive)
                .IgnoreMember(dest => dest.Status);

            TypeAdapterConfig<Model.Tip, Model.Tip>
                .NewConfig()
                .IgnoreMember(dest => dest.Id)
                .IgnoreMember(dest => dest.CreatedBy)
                .IgnoreMember(dest => dest.CreatedOn)
                .IgnoreMember(dest => dest.ModifiedBy)
                .IgnoreMember(dest => dest.ModifiedOn)
                .IgnoreMember(dest => dest.IsActive)
                .IgnoreMember(dest => dest.Status);

            TypeAdapterConfig<TipRequest, Model.Tip>
                .NewConfig();

            TypeAdapterConfig<Model.Tip, TipResponse>
                .NewConfig();

            #endregion

            #region Dealer

            TypeAdapterConfig<Model.Dealer, DTO.Dealer>
                .NewConfig();

            TypeAdapterConfig<DTO.Dealer, Model.Dealer>
                .NewConfig()
                .IgnoreMember(dest => dest.CreatedBy)
                .IgnoreMember(dest => dest.CreatedOn)
                .IgnoreMember(dest => dest.ModifiedBy)
                .IgnoreMember(dest => dest.ModifiedOn)
                .IgnoreMember(dest => dest.IsActive)
                .IgnoreMember(dest => dest.Status);

            TypeAdapterConfig<Model.Dealer, Model.Dealer>
                .NewConfig()
                .IgnoreMember(dest => dest.Id)
                .IgnoreMember(dest => dest.CreatedBy)
                .IgnoreMember(dest => dest.CreatedOn)
                .IgnoreMember(dest => dest.ModifiedBy)
                .IgnoreMember(dest => dest.ModifiedOn)
                .IgnoreMember(dest => dest.IsActive)
                .IgnoreMember(dest => dest.Status);

            TypeAdapterConfig<DealerRequest, Model.Dealer>
                .NewConfig();

            TypeAdapterConfig<Model.Dealer, DealerResponse>
                .NewConfig();

            #endregion

            #region Saucer

            TypeAdapterConfig<Model.Saucer, DTO.Saucer>
                .NewConfig();

            TypeAdapterConfig<DTO.Saucer, Model.Saucer>
                .NewConfig()
                .IgnoreMember(dest => dest.CreatedBy)
                .IgnoreMember(dest => dest.CreatedOn)
                .IgnoreMember(dest => dest.ModifiedBy)
                .IgnoreMember(dest => dest.ModifiedOn)
                .IgnoreMember(dest => dest.IsActive)
                .IgnoreMember(dest => dest.Status);

            TypeAdapterConfig<Model.Saucer, Model.Saucer>
                .NewConfig()
                .IgnoreMember(dest => dest.Id)
                .IgnoreMember(dest => dest.CreatedBy)
                .IgnoreMember(dest => dest.CreatedOn)
                .IgnoreMember(dest => dest.ModifiedBy)
                .IgnoreMember(dest => dest.ModifiedOn)
                .IgnoreMember(dest => dest.IsActive)
                .IgnoreMember(dest => dest.Status);

            TypeAdapterConfig<SaucerRequest, Model.Saucer>
                .NewConfig();

            TypeAdapterConfig<Model.Saucer, SaucerResponse>
                .NewConfig();

            #endregion

            #region SaucerMultimedia

            TypeAdapterConfig<Model.SaucerMultimedia, DTO.SaucerMultimedia>
                .NewConfig()
                .MapFrom(dest => dest.Path, src => MapperResolver.MultimediaPath(src.Path));

            TypeAdapterConfig<DTO.SaucerMultimedia, Model.SaucerMultimedia>
                .NewConfig()
                .IgnoreMember(dest => dest.CreatedBy)
                .IgnoreMember(dest => dest.CreatedOn)
                .IgnoreMember(dest => dest.ModifiedBy)
                .IgnoreMember(dest => dest.ModifiedOn)
                .IgnoreMember(dest => dest.IsActive)
                .IgnoreMember(dest => dest.Status);

            TypeAdapterConfig<Model.SaucerMultimedia, Model.SaucerMultimedia>
                .NewConfig()
                .IgnoreMember(dest => dest.Path)
                .IgnoreMember(dest => dest.Id)
                .IgnoreMember(dest => dest.CreatedBy)
                .IgnoreMember(dest => dest.CreatedOn)
                .IgnoreMember(dest => dest.ModifiedBy)
                .IgnoreMember(dest => dest.ModifiedOn)
                .IgnoreMember(dest => dest.IsActive)
                .IgnoreMember(dest => dest.Status);

            TypeAdapterConfig<SaucerMultimediaRequest, Model.SaucerMultimedia>
                .NewConfig();

            TypeAdapterConfig<Model.SaucerMultimedia, SaucerMultimediaResponse>
                .NewConfig()
                .MapFrom(dest => dest.Path, src => MapperResolver.MultimediaPath(src.Path));

            #endregion

            #region IngredientGroup

            TypeAdapterConfig<Model.IngredientGroup, DTO.IngredientGroup>
                .NewConfig();

            TypeAdapterConfig<DTO.IngredientGroup, Model.IngredientGroup>
                .NewConfig()
                .IgnoreMember(dest => dest.CreatedBy)
                .IgnoreMember(dest => dest.CreatedOn)
                .IgnoreMember(dest => dest.ModifiedBy)
                .IgnoreMember(dest => dest.ModifiedOn)
                .IgnoreMember(dest => dest.IsActive)
                .IgnoreMember(dest => dest.Status);

            TypeAdapterConfig<Model.IngredientGroup, Model.IngredientGroup>
                .NewConfig()
                .IgnoreMember(dest => dest.Id)
                .IgnoreMember(dest => dest.CreatedBy)
                .IgnoreMember(dest => dest.CreatedOn)
                .IgnoreMember(dest => dest.ModifiedBy)
                .IgnoreMember(dest => dest.ModifiedOn)
                .IgnoreMember(dest => dest.IsActive)
                .IgnoreMember(dest => dest.Status);

            TypeAdapterConfig<IngredientGroupRequest, Model.IngredientGroup>
                .NewConfig();

            TypeAdapterConfig<Model.IngredientGroup, IngredientGroupResponse>
                .NewConfig();

            #endregion

            #region Ingredient

            TypeAdapterConfig<Model.Ingredient, DTO.Ingredient>
                .NewConfig();

            TypeAdapterConfig<DTO.Ingredient, Model.Ingredient>
                .NewConfig()
                .IgnoreMember(dest => dest.CreatedBy)
                .IgnoreMember(dest => dest.CreatedOn)
                .IgnoreMember(dest => dest.ModifiedBy)
                .IgnoreMember(dest => dest.ModifiedOn)
                .IgnoreMember(dest => dest.IsActive)
                .IgnoreMember(dest => dest.Status);

            TypeAdapterConfig<Model.Ingredient, Model.Ingredient>
                .NewConfig()
                .IgnoreMember(dest => dest.Id)
                .IgnoreMember(dest => dest.CreatedBy)
                .IgnoreMember(dest => dest.CreatedOn)
                .IgnoreMember(dest => dest.ModifiedBy)
                .IgnoreMember(dest => dest.ModifiedOn)
                .IgnoreMember(dest => dest.IsActive)
                .IgnoreMember(dest => dest.Status);

            TypeAdapterConfig<IngredientRequest, Model.Ingredient>
                .NewConfig();

            TypeAdapterConfig<Model.Ingredient, IngredientResponse>
                .NewConfig();

            #endregion

            #region GenericEntityRelation

            TypeAdapterConfig<RelationRequest, Model.BranchDealer>
                .NewConfig()
                .MapFrom(dest => dest.BranchId, src => src.FirstReference)
                .MapFrom(dest => dest.DealerId, src => src.SecondReference);

            TypeAdapterConfig<RelationRequest, Model.DealerSaucer>
                .NewConfig()
                .MapFrom(dest => dest.DealerId, src => src.FirstReference)
                .MapFrom(dest => dest.SaucerId, src => src.SecondReference);

            #endregion

            #region SaucerConfiguration

            TypeAdapterConfig<Model.SaucerConfiguration, DTO.SaucerConfiguration>
                .NewConfig();

            TypeAdapterConfig<DTO.SaucerConfiguration, Model.SaucerConfiguration>
                .NewConfig()
                .IgnoreMember(dest => dest.CreatedBy)
                .IgnoreMember(dest => dest.CreatedOn)
                .IgnoreMember(dest => dest.ModifiedBy)
                .IgnoreMember(dest => dest.ModifiedOn)
                .IgnoreMember(dest => dest.IsActive)
                .IgnoreMember(dest => dest.Status);

            TypeAdapterConfig<Model.SaucerConfiguration, Model.SaucerConfiguration>
                .NewConfig()
                .IgnoreMember(dest => dest.Id)
                .IgnoreMember(dest => dest.CreatedBy)
                .IgnoreMember(dest => dest.CreatedOn)
                .IgnoreMember(dest => dest.ModifiedBy)
                .IgnoreMember(dest => dest.ModifiedOn)
                .IgnoreMember(dest => dest.IsActive)
                .IgnoreMember(dest => dest.Status);

            TypeAdapterConfig<SaucerConfigurationRequest, Model.SaucerConfiguration>
                .NewConfig();

            TypeAdapterConfig<Model.SaucerConfiguration, SaucerConfigurationResponse>
                .NewConfig();

            #endregion

            #region Worker

            TypeAdapterConfig<Model.Worker, DTO.Worker>
                .NewConfig();

            TypeAdapterConfig<DTO.Worker, Model.Worker>
                .NewConfig()
                .IgnoreMember(dest => dest.CreatedBy)
                .IgnoreMember(dest => dest.CreatedOn)
                .IgnoreMember(dest => dest.ModifiedBy)
                .IgnoreMember(dest => dest.ModifiedOn)
                .IgnoreMember(dest => dest.IsActive)
                .IgnoreMember(dest => dest.Status);

            TypeAdapterConfig<Model.Worker, Model.Worker>
                .NewConfig()
                .IgnoreMember(dest => dest.Id)
                .IgnoreMember(dest => dest.CreatedBy)
                .IgnoreMember(dest => dest.CreatedOn)
                .IgnoreMember(dest => dest.ModifiedBy)
                .IgnoreMember(dest => dest.ModifiedOn)
                .IgnoreMember(dest => dest.IsActive)
                .IgnoreMember(dest => dest.Status)
                .IgnoreMember(dest => dest.PublicKey)
                .IgnoreMember(dest => dest.Time);

            TypeAdapterConfig<WorkerRequest, Model.Worker>
                .NewConfig();

            TypeAdapterConfig<Model.Worker, WorkerResponse>
                .NewConfig();

            #endregion

            #region Menu

            TypeAdapterConfig<Model.Menu, DTO.Menu>
                .NewConfig()
                .MapFrom(dest => dest.StartDate, src => src.StartDate.ToDateString())
                .MapFrom(dest => dest.EndDate, src => src.EndDate.ToDateString());

            TypeAdapterConfig<DTO.Menu, Model.Menu>
                .NewConfig()
                .IgnoreMember(dest => dest.CreatedBy)
                .IgnoreMember(dest => dest.CreatedOn)
                .IgnoreMember(dest => dest.ModifiedBy)
                .IgnoreMember(dest => dest.ModifiedOn)
                .IgnoreMember(dest => dest.IsActive)
                .IgnoreMember(dest => dest.Status);

            TypeAdapterConfig<Model.Menu, Model.Menu>
                .NewConfig()
                .IgnoreMember(dest => dest.Id)
                .IgnoreMember(dest => dest.CreatedBy)
                .IgnoreMember(dest => dest.CreatedOn)
                .IgnoreMember(dest => dest.ModifiedBy)
                .IgnoreMember(dest => dest.ModifiedOn)
                .IgnoreMember(dest => dest.IsActive)
                .IgnoreMember(dest => dest.Status);

            TypeAdapterConfig<MenuRequest, Model.Menu>
                .NewConfig();

            TypeAdapterConfig<Model.Menu, MenuResponse>
                .NewConfig()
                .MapFrom(dest => dest.StartDate, src => src.StartDate.ToDateString())
                .MapFrom(dest => dest.EndDate, src => src.EndDate.ToDateString());

            #endregion

            #region Reservation

            TypeAdapterConfig<Model.Reservation, DTO.Reservation>
                .NewConfig()
                .MapFrom(dest => dest.Date, src => src.Date.ToDateString());

            TypeAdapterConfig<DTO.Reservation, Model.Reservation>
                .NewConfig()
                .IgnoreMember(dest => dest.CreatedBy)
                .IgnoreMember(dest => dest.CreatedOn)
                .IgnoreMember(dest => dest.ModifiedBy)
                .IgnoreMember(dest => dest.ModifiedOn)
                .IgnoreMember(dest => dest.IsActive)
                .IgnoreMember(dest => dest.Status);

            TypeAdapterConfig<Model.Reservation, Model.Reservation>
                .NewConfig()
                .IgnoreMember(dest => dest.Id)
                .IgnoreMember(dest => dest.CreatedBy)
                .IgnoreMember(dest => dest.CreatedOn)
                .IgnoreMember(dest => dest.ModifiedBy)
                .IgnoreMember(dest => dest.ModifiedOn)
                .IgnoreMember(dest => dest.IsActive)
                .IgnoreMember(dest => dest.Status);

            TypeAdapterConfig<ReservationRequest, Model.Reservation>
                .NewConfig()
                .MapFrom(dest => dest.Date, src => src.Date.DateStringToDateTime())
                .IgnoreMember(dest => dest.Energy)
                .IgnoreMember(dest => dest.Protein)
                .IgnoreMember(dest => dest.Carbohydrate)
                .IgnoreMember(dest => dest.Sugar)
                .IgnoreMember(dest => dest.Lipid)
                .IgnoreMember(dest => dest.Sodium);

            TypeAdapterConfig<Model.Reservation, ReservationResponse>
                .NewConfig()
                .MapFrom(dest => dest.Date, src => src.Date.ToDateString());

            #endregion

            #region Role

            TypeAdapterConfig<Model.Role, DTO.Role>
                .NewConfig();

            TypeAdapterConfig<DTO.Role, Model.Role>
                .NewConfig()
                .IgnoreMember(dest => dest.CreatedBy)
                .IgnoreMember(dest => dest.CreatedOn)
                .IgnoreMember(dest => dest.ModifiedBy)
                .IgnoreMember(dest => dest.ModifiedOn)
                .IgnoreMember(dest => dest.IsActive)
                .IgnoreMember(dest => dest.Status);

            TypeAdapterConfig<Model.Role, Model.Role>
                .NewConfig()
                .IgnoreMember(dest => dest.Id)
                .IgnoreMember(dest => dest.CreatedBy)
                .IgnoreMember(dest => dest.CreatedOn)
                .IgnoreMember(dest => dest.ModifiedBy)
                .IgnoreMember(dest => dest.ModifiedOn)
                .IgnoreMember(dest => dest.IsActive)
                .IgnoreMember(dest => dest.Status);

            TypeAdapterConfig<RoleRequest, Model.Role>
                .NewConfig();

            TypeAdapterConfig<Model.Role, RoleResponse>
                .NewConfig();

            #endregion

            #region RoleConfiguration

            TypeAdapterConfig<Model.RoleConfiguration, DTO.RoleConfiguration>
                .NewConfig();

            TypeAdapterConfig<DTO.RoleConfiguration, Model.RoleConfiguration>
                .NewConfig()
                .IgnoreMember(dest => dest.CreatedBy)
                .IgnoreMember(dest => dest.CreatedOn)
                .IgnoreMember(dest => dest.ModifiedBy)
                .IgnoreMember(dest => dest.ModifiedOn)
                .IgnoreMember(dest => dest.IsActive)
                .IgnoreMember(dest => dest.Status);

            TypeAdapterConfig<Model.RoleConfiguration, Model.RoleConfiguration>
                .NewConfig()
                .IgnoreMember(dest => dest.Id)
                .IgnoreMember(dest => dest.CreatedBy)
                .IgnoreMember(dest => dest.CreatedOn)
                .IgnoreMember(dest => dest.ModifiedBy)
                .IgnoreMember(dest => dest.ModifiedOn)
                .IgnoreMember(dest => dest.IsActive)
                .IgnoreMember(dest => dest.Status);

            TypeAdapterConfig<RoleConfigurationRequest, Model.RoleConfiguration>
                .NewConfig();

            TypeAdapterConfig<Model.RoleConfiguration, RoleConfigurationResponse>
                .NewConfig();

            #endregion

            #region Permission

            TypeAdapterConfig<Model.Permission, DTO.Permission>
                .NewConfig();

            TypeAdapterConfig<DTO.Permission, Model.Permission>
                .NewConfig();

            TypeAdapterConfig<Model.Permission, Model.Permission>
                .NewConfig()
                .IgnoreMember(dest => dest.Id);

            #endregion

            #region AccessLevel

            TypeAdapterConfig<Model.AccessLevel, DTO.AccessLevel>
                .NewConfig();

            TypeAdapterConfig<DTO.AccessLevel, Model.AccessLevel>
                .NewConfig();

            TypeAdapterConfig<Model.AccessLevel, Model.AccessLevel>
                .NewConfig()
                .IgnoreMember(dest => dest.Id);

            TypeAdapterConfig<Model.AccessLevel, AccessLevelResponse>
                .NewConfig();

            #endregion
        }
    }
}