using FoodManager.DTO;
using FoodManager.DTO.BaseRequest;
using FoodManager.DTO.BaseResponse;
using FoodManager.DTO.Message.Ingredients;

namespace FoodManager.Services.Interfaces
{
    public interface IIngredientService
    {
        FindIngredientsResponse Find(FindIngredientsRequest request);
        CreateResponse Create(IngredientRequest request);
        SuccessResponse Update(IngredientRequest request);
        Ingredient Get(GetIngredientRequest request);
        SuccessResponse Delete(DeleteIngredientRequest request);
        SuccessResponse ChangeStatus(ChangeStatusRequest request);
        SuccessResponse IsReference(IsReferenceRequest request);
    }
}