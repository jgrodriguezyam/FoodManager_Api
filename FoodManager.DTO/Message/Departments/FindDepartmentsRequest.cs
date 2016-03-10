using FoodManager.DTO.BaseRequest;

namespace FoodManager.DTO.Message.Departments
{
    public class FindDepartmentsRequest : FindStatusRequest
    {
         public int BranchId { get; set; }
    }
}