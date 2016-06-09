using System.Web.Http;
using FoodManager.DTO.BaseResponse;
using FoodManager.DTO.Message.AccessLevels;
using FoodManager.DTO.Message.RoleConfigurations;
using FoodManager.Infrastructure.Enums;
using FoodManager.Infrastructure.Roles;
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
        public RoleConfigurationResponse Get(GetRoleConfigurationRequest request)
        {
            return _roleConfigurationService.Get(request);
        }

        [HttpDelete, Route("role-configurations/{Id}")]
        public SuccessResponse Delete(DeleteRoleConfigurationRequest request)
        {
            return _roleConfigurationService.Delete(request);
        }

        [HttpGet, Route("role-configurations/permissions")]
        public EnumeratorResponse Get()
        {
            return new EnumeratorResponse { Enumerator = new PermissionType().ConvertToCollection() };
        }

        [HttpGet, Route("role-configurations/access-levels")]
        public FindAccessLevelsResponse Get(FindAccessLevelsRequest request)
        {
            return _roleConfigurationService.Find(request);
        }
    }
}