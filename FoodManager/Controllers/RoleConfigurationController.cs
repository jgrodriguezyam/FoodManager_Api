using System.Web.Http;
using FoodManager.DTO;
using FoodManager.DTO.BaseResponse;
using FoodManager.DTO.Message.RoleConfigurations;
using FoodManager.Services.Interfaces;

namespace FoodManager.Controllers
{
    public class RoleConfigurationController : ApiController
    {
        private readonly IRoleConfigurationService _roleConfigurationService;

        public RoleConfigurationController(IRoleConfigurationService roleConfigurationService)
        {
            _roleConfigurationService = roleConfigurationService;
        }

        [HttpGet, Route("role-configurations")]
        public FindRoleConfigurationsResponse Get(FindRoleConfigurationsRequest request)
        {
            return _roleConfigurationService.Find(request);
        }

        [HttpPost, Route("role-configurations")]
        public CreateResponse Post(RoleConfigurationRequest request)
        {
            return _roleConfigurationService.Create(request);
        }

        [HttpPut, Route("role-configurations")]
        public SuccessResponse Put(RoleConfigurationRequest request)
        {
            return _roleConfigurationService.Update(request);
        }

        [HttpGet, Route("role-configurations/{Id}")]
        public RoleConfiguration Get(GetRoleConfigurationRequest request)
        {
            return _roleConfigurationService.Get(request);
        }

        [HttpDelete, Route("role-configurations/{Id}")]
        public SuccessResponse Delete(DeleteRoleConfigurationRequest request)
        {
            return _roleConfigurationService.Delete(request);
        }
    }
}