using System.Web.Http;
using FoodManager.DTO;
using FoodManager.DTO.BaseRequest;
using FoodManager.DTO.BaseResponse;
using FoodManager.DTO.Message.SaucerConfigurations;
using FoodManager.Services.Interfaces;

namespace FoodManager.Controllers
{
    public class SaucerConfigurationController : ApiController
    {
        private readonly ISaucerConfigurationService _saucerConfigurationService;

        public SaucerConfigurationController(ISaucerConfigurationService saucerConfigurationService)
        {
            _saucerConfigurationService = saucerConfigurationService;
        }

        [HttpGet, Route("saucer-configurations")]
        public FindSaucerConfigurationsResponse Get(FindSaucerConfigurationsRequest request)
        {
            return _saucerConfigurationService.Find(request);
        }

        [HttpPost, Route("saucer-configurations")]
        public CreateResponse Post(SaucerConfigurationRequest request)
        {
            return _saucerConfigurationService.Create(request);
        }

        [HttpPut, Route("saucer-configurations")]
        public SuccessResponse Put(SaucerConfigurationRequest request)
        {
            return _saucerConfigurationService.Update(request);
        }

        [HttpGet, Route("saucer-configurations/{Id}")]
        public SaucerConfiguration Get(GetSaucerConfigurationRequest request)
        {
            return _saucerConfigurationService.Get(request);
        }

        [HttpDelete, Route("saucer-configurations/{Id}")]
        public SuccessResponse Delete(DeleteSaucerConfigurationRequest request)
        {
            return _saucerConfigurationService.Delete(request);
        }

        [HttpPut, Route("saucer-configurations/{Id}/status/{Status}")]
        public SuccessResponse Put(ChangeStatusRequest request)
        {
            return _saucerConfigurationService.ChangeStatus(request);
        }
    }
}