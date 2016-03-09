using FoodManager.DTO;
using FoodManager.DTO.BaseRequest;
using FoodManager.DTO.BaseResponse;
using FoodManager.DTO.Message.Regions;

namespace FoodManager.Services.Interfaces
{
    public interface IRegionService
    {
        FindRegionsResponse Find(FindRegionsRequest request);
        CreateResponse Create(RegionRequest request);
        SuccessResponse Update(RegionRequest request);
        Region Get(GetRegionRequest request);
        SuccessResponse Delete(DeleteRegionRequest request);
        SuccessResponse ChangeStatus(ChangeStatusRequest request);
    }
}