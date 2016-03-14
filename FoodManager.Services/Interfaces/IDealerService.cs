using FoodManager.DTO;
using FoodManager.DTO.BaseRequest;
using FoodManager.DTO.BaseResponse;
using FoodManager.DTO.Message.Dealers;

namespace FoodManager.Services.Interfaces
{
    public interface IDealerService
    {
        FindDealersResponse Find(FindDealersRequest request);
        CreateResponse Create(DealerRequest request);
        SuccessResponse Update(DealerRequest request);
        Dealer Get(GetDealerRequest request);
        SuccessResponse Delete(DeleteDealerRequest request);
        SuccessResponse ChangeStatus(ChangeStatusRequest request);
        SuccessResponse IsReference(IsReferenceRequest request);
        SuccessResponse AddSaucer(RelationRequest request);
        SuccessResponse RemoveSaucer(RelationRequest request);
    }
}