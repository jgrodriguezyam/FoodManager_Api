using FoodManager.DTO.BaseRequest;

namespace FoodManager.DTO.Message.Users
{
    public class FindUsersRequest : FindStatusRequest
    {
        public string Name { get; set; }
        public string UserName { get; set; }
        public int DealerId { get; set; }
    }
}