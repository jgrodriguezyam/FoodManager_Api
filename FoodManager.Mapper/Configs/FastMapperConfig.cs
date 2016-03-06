using FastMapper;
using FoodManager.DTO.Message.Branches;
using FoodManager.DTO.Message.Companies;
using FoodManager.DTO.Message.Departments;
using FoodManager.DTO.Message.Regions;

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
                .IgnoreMember(dest => dest.IsActive);

            TypeAdapterConfig<Model.Region, Model.Region>
                .NewConfig()
                .IgnoreMember(dest => dest.Id)
                .IgnoreMember(dest => dest.CreatedBy)
                .IgnoreMember(dest => dest.CreatedOn)
                .IgnoreMember(dest => dest.ModifiedBy)
                .IgnoreMember(dest => dest.ModifiedOn)
                .IgnoreMember(dest => dest.IsActive);

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
                .IgnoreMember(dest => dest.IsActive);

            TypeAdapterConfig<Model.Company, Model.Company>
                .NewConfig()
                .IgnoreMember(dest => dest.Id)
                .IgnoreMember(dest => dest.CreatedBy)
                .IgnoreMember(dest => dest.CreatedOn)
                .IgnoreMember(dest => dest.ModifiedBy)
                .IgnoreMember(dest => dest.ModifiedOn)
                .IgnoreMember(dest => dest.IsActive);

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
                .IgnoreMember(dest => dest.IsActive);

            TypeAdapterConfig<Model.Branch, Model.Branch>
                .NewConfig()
                .IgnoreMember(dest => dest.Id)
                .IgnoreMember(dest => dest.CreatedBy)
                .IgnoreMember(dest => dest.CreatedOn)
                .IgnoreMember(dest => dest.ModifiedBy)
                .IgnoreMember(dest => dest.ModifiedOn)
                .IgnoreMember(dest => dest.IsActive);

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
                .IgnoreMember(dest => dest.IsActive);

            TypeAdapterConfig<Model.Department, Model.Department>
                .NewConfig()
                .IgnoreMember(dest => dest.Id)
                .IgnoreMember(dest => dest.CreatedBy)
                .IgnoreMember(dest => dest.CreatedOn)
                .IgnoreMember(dest => dest.ModifiedBy)
                .IgnoreMember(dest => dest.ModifiedOn)
                .IgnoreMember(dest => dest.IsActive);

            TypeAdapterConfig<DepartmentRequest, Model.Department>
                .NewConfig();

            TypeAdapterConfig<Model.Department, DepartmentResponse>
                .NewConfig();

            #endregion
        }
    }
}