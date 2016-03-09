using FastMapper;
using FoodManager.DTO.Message.Branches;
using FoodManager.DTO.Message.Companies;
using FoodManager.DTO.Message.Departments;
using FoodManager.DTO.Message.Diseases;
using FoodManager.DTO.Message.Regions;
using FoodManager.DTO.Message.Users;
using FoodManager.DTO.Message.Warnings;

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
        }
    }
}