using System.Web.Http;
using FoodManager.DTO;
using FoodManager.DTO.BaseRequest;
using FoodManager.DTO.BaseResponse;
using FoodManager.DTO.Message.Saucers;
using FoodManager.Services.Interfaces;

namespace FoodManager.Controllers
{
    public class SaucerController : ApiController
    {
        private readonly ISaucerService _saucerService;

        public SaucerController(ISaucerService saucerService)
        {
            _saucerService = saucerService;
        }

        [HttpGet, Route("saucers")]
        public FindSaucersResponse Get(FindSaucersRequest request)
        {
            return _saucerService.Find(request);
        }

        [HttpPost, Route("saucers")]
        public CreateResponse Post(SaucerRequest request)
        {
            return _saucerService.Create(request);
        }

        [HttpPut, Route("saucers")]
        public SuccessResponse Put(SaucerRequest request)
        {
            return _saucerService.Update(request);
        }

        [HttpGet, Route("saucers/{Id}")]
        public Saucer Get(GetSaucerRequest request)
        {
            return _saucerService.Get(request);
        }

        [HttpDelete, Route("saucers/{Id}")]
        public SuccessResponse Delete(DeleteSaucerRequest request)
        {
            return _saucerService.Delete(request);
        }

        [HttpPut, Route("saucers/{Id}/status/{Status}")]
        public SuccessResponse Put(ChangeStatusRequest request)
        {
            return _saucerService.ChangeStatus(request);
        }

        [HttpGet, Route("saucers/is-reference/{Id}")]
        public SuccessResponse Get(IsReferenceRequest request)
        {
            return _saucerService.IsReference(request);
        }
    }
}