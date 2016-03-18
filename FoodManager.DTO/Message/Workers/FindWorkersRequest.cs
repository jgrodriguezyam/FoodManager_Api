using FoodManager.DTO.BaseRequest;

namespace FoodManager.DTO.Message.Workers
{
    public class FindWorkersRequest : FindStatusRequest
    {
        public int DepartmentId { get; set; }
        public int JobId { get; set; }
        public int DealerId { get; set; }
    }
}