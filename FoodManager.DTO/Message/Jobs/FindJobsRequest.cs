using FoodManager.DTO.BaseRequest;

namespace FoodManager.DTO.Message.Jobs
{
    public class FindJobsRequest : FindStatusRequest
    {
        public string Name { get; set; }
    }
}