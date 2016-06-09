using FoodManager.DTO.BaseRequest;
using FoodManager.DTO.BaseResponse;
using FoodManager.DTO.Message.Departments;

namespace FoodManager.Services.Interfaces
{
    public interface IDepartmentService
    {
        FindDepartmentsResponse Find(FindDepartmentsRequest request);
        CreateResponse Create(DepartmentRequest request);
        SuccessResponse Update(DepartmentRequest request);
        DepartmentResponse Get(GetDepartmentRequest request);
        SuccessResponse Delete(DeleteDepartmentRequest request);
        SuccessResponse ChangeStatus(ChangeStatusRequest request);
        SuccessResponse IsReference(IsReferenceRequest request);
    }
}