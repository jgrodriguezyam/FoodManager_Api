using FoodManager.DTO;
using FoodManager.DTO.BaseRequest;
using FoodManager.DTO.BaseResponse;
using FoodManager.DTO.Message.IngredientGroups;

namespace FoodManager.Services.Interfaces
{
    public interface IIngredientGroupService
    {
        FindIngredientGroupsResponse Find(FindIngredientGroupsRequest request);
        CreateResponse Create(IngredientGroupRequest request);
        SuccessResponse Update(IngredientGroupRequest request);
        IngredientGroup Get(GetIngredientGroupRequest request);
        SuccessResponse Delete(DeleteIngredientGroupRequest request);
        SuccessResponse ChangeStatus(ChangeStatusRequest request);
        SuccessResponse IsReference(IsReferenceRequest request);
    }
}