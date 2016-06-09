using System.Web.Http;
using FoodManager.DTO.BaseRequest;
using FoodManager.DTO.BaseResponse;
using FoodManager.DTO.Message.Departments;
using FoodManager.Services.Interfaces;

namespace FoodManager.Controllers
{
    public class DepartmentController : ApiController
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpGet, Route("departments")]
        public FindDepartmentsResponse Get(FindDepartmentsRequest request)
        {
            return _departmentService.Find(request);
        }

        [HttpPost, Route("departments")]
        public CreateResponse Post(DepartmentRequest request)
        {
            return _departmentService.Create(request);
        }

        [HttpPut, Route("departments")]
        public SuccessResponse Put(DepartmentRequest request)
        {
            return _departmentService.Update(request);
        }

        [HttpGet, Route("departments/{Id}")]
        public DepartmentResponse Get(GetDepartmentRequest request)
        {
            return _departmentService.Get(request);
        }

        [HttpDelete, Route("departments/{Id}")]
        public SuccessResponse Delete(DeleteDepartmentRequest request)
        {
            return _departmentService.Delete(request);
        }

        [HttpPut, Route("departments/{Id}/status/{Status}")]
        public SuccessResponse ChangeStatus(ChangeStatusRequest request)
        {
            return _departmentService.ChangeStatus(request);
        }

        [HttpGet, Route("departments/is-reference/{Id}")]
        public SuccessResponse Get(IsReferenceRequest request)
        {
            return _departmentService.IsReference(request);
        }
    }
}