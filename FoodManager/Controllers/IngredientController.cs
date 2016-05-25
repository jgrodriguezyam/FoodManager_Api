using System.Web.Http;
using FoodManager.DTO;
using FoodManager.DTO.BaseRequest;
using FoodManager.DTO.BaseResponse;
using FoodManager.DTO.Message.Ingredients;
using FoodManager.Infrastructure.Enums;
using FoodManager.Model.Enums;
using FoodManager.Services.Interfaces;

namespace FoodManager.Controllers
{
    public class IngredientController : ApiController
    {
        private readonly IIngredientService _ingredientService;

        public IngredientController(IIngredientService ingredientService)
        {
            _ingredientService = ingredientService;
        }

        [HttpGet, Route("ingredients")]
        public FindIngredientsResponse Get(FindIngredientsRequest request)
        {
            return _ingredientService.Find(request);
        }

        [HttpPost, Route("ingredients")]
        public CreateResponse Post(IngredientRequest request)
        {
            return _ingredientService.Create(request);
        }

        [HttpPut, Route("ingredients")]
        public SuccessResponse Put(IngredientRequest request)
        {
            return _ingredientService.Update(request);
        }

        [HttpGet, Route("ingredients/{Id}")]
        public Ingredient Get(GetIngredientRequest request)
        {
            return _ingredientService.Get(request);
        }

        [HttpDelete, Route("ingredients/{Id}")]
        public SuccessResponse Delete(DeleteIngredientRequest request)
        {
            return _ingredientService.Delete(request);
        }

        [HttpPut, Route("ingredients/{Id}/status/{Status}")]
        public SuccessResponse Put(ChangeStatusRequest request)
        {
            return _ingredientService.ChangeStatus(request);
        }

        [HttpGet, Route("ingredients/is-reference/{Id}")]
        public SuccessResponse Get(IsReferenceRequest request)
        {
            return _ingredientService.IsReference(request);
        }

        [HttpGet, Route("ingredients/units")]
        public EnumeratorResponse Get()
        {
            return new EnumeratorResponse { Enumerator = new UnitType().ConvertToCollection() };
        }
    }
}