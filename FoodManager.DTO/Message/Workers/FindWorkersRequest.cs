using FoodManager.DTO.BaseRequest;

namespace FoodManager.DTO.Message.Workers
{
    public class FindWorkersRequest : FindStatusRequest
    {
        public int DepartmentId { get; set; }
        public int JobId { get; set; }
        public int DealerId { get; set; }
        public int RoleId { get; set; }
        public int BranchId { get; set; }
        public string Code { get; set; }
        public string Email { get; set; }
        public string Badge { get; set; }
        public string Imss { get; set; }
    }
}