using FastMapper;
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
        }
    }
}