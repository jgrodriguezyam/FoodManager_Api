using System.Web.Http;
using FoodManager.DTO;
using FoodManager.DTO.BaseRequest;
using FoodManager.DTO.BaseResponse;
using FoodManager.DTO.Message.Regions;
using FoodManager.Services.Interfaces;

namespace FoodManager.Controllers
{
    public class RegionController : ApiController
    {
        private readonly IRegionService _regionService;

        public RegionController(IRegionService regionService)
        {
            _regionService = regionService;
        }

        [HttpGet, Route("regions")]
        public FindRegionsResponse Get(FindRegionsRequest request)
        {
            return _regionService.Find(request);
        }

        [HttpPost, Route("regions")]
        public CreateResponse Post(RegionRequest request)
        {
            return _regionService.Create(request);
        }

        [HttpPut, Route("regions")]
        public SuccessResponse Put(RegionRequest request)
        {
            return _regionService.Update(request);
        }

        [HttpGet, Route("regions/{Id}")]
        public Region Get(GetRegionRequest request)
        {
            return _regionService.Get(request);
        }

        [HttpDelete, Route("regions/{Id}")]
        public SuccessResponse Delete(DeleteRegionRequest request)
        {
            return _regionService.Delete(request);
        }

        [HttpPut, Route("regions/{Id}/status/{Status}")]
        public SuccessResponse Put(ChangeStatusRequest request)
        {
            return _regionService.ChangeStatus(request);
        }

        [HttpGet, Route("regions/is-reference/{Id}")]
        public SuccessResponse Get(IsReferenceRequest request)
        {
            return _regionService.IsReference(request);
        }
    }
}