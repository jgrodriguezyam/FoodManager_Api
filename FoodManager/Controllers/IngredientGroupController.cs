using System.Web.Http;
using FoodManager.DTO.BaseRequest;
using FoodManager.DTO.BaseResponse;
using FoodManager.DTO.Message.IngredientGroups;
using FoodManager.Services.Interfaces;
using FoodManager.Helpers;

namespace FoodManager.Controllers
{
    public class IngredientGroupController : ApiController
    {
        private readonly IIngredientGroupService _ingredientGroupService;

        public IngredientGroupController(IIngredientGroupService ingredientGroupService)
        {
            _ingredientGroupService = ingredientGroupService;
        }

        [HttpGet, Route("ingredient-groups")]
        public FindIngredientGroupsResponse Get(FindIngredientGroupsRequest request)
        {
            return _ingredientGroupService.Find(request);
        }

        [HttpPost, Route("ingredient-groups")]
        public CreateResponse Post(IngredientGroupRequest request)
        {
            return _ingredientGroupService.Create(request);
        }

        [HttpPut, Route("ingredient-groups")]
        public SuccessResponse Put(IngredientGroupRequest request)
        {
            return _ingredientGroupService.Update(request);
        }

        [HttpGet, Route("ingredient-groups/{Id}")]
        public IngredientGroupResponse Get(GetIngredientGroupRequest request)
        {
            return _ingredientGroupService.Get(request);
        }

        [HttpDelete, Route("ingredient-groups/{Id}")]
        public SuccessResponse Delete(DeleteIngredientGroupRequest request)
        {
            return _ingredientGroupService.Delete(request);
        }

        [HttpPut, Route("ingredient-groups/{Id}/status/{Status}")]
        public SuccessResponse ChangeStatus(ChangeStatusRequest request)
        {
            return _ingredientGroupService.ChangeStatus(request);
        }

        [HttpGet, Route("ingredient-groups/is-reference/{Id}")]
        public SuccessResponse Get(IsReferenceRequest request)
        {
            return _ingredientGroupService.IsReference(request);
        }

        [HttpPost, Route("ingredient-groups/csv")]
        public SuccessResponse Post(CsvRequest request)
        {
            return _ingredientGroupService.Csv(request, Request.GetFile());
        }
    }
}