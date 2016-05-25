using System.Web.Http;
using FoodManager.DTO;
using FoodManager.DTO.BaseRequest;
using FoodManager.DTO.BaseResponse;
using FoodManager.DTO.Message.Roles;
using FoodManager.Services.Interfaces;

namespace FoodManager.Controllers
{
    public class RoleController : ApiController
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet, Route("roles")]
        public FindRolesResponse Get(FindRolesRequest request)
        {
            return _roleService.Find(request);
        }

        [HttpPost, Route("roles")]
        public CreateResponse Post(RoleRequest request)
        {
            return _roleService.Create(request);
        }

        [HttpPut, Route("roles")]
        public SuccessResponse Put(RoleRequest request)
        {
            return _roleService.Update(request);
        }

        [HttpGet, Route("roles/{Id}")]
        public Role Get(GetRoleRequest request)
        {
            return _roleService.Get(request);
        }

        [HttpDelete, Route("roles/{Id}")]
        public SuccessResponse Delete(DeleteRoleRequest request)
        {
            return _roleService.Delete(request);
        }

        [HttpPut, Route("roles/{Id}/status/{Status}")]
        public SuccessResponse Put(ChangeStatusRequest request)
        {
            return _roleService.ChangeStatus(request);
        }

        [HttpGet, Route("roles/is-reference/{Id}")]
        public SuccessResponse Get(IsReferenceRequest request)
        {
            return _roleService.IsReference(request);
        }
    }
}