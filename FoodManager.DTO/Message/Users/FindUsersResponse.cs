using System.Collections.Generic;
using FoodManager.DTO.BaseResponse;

namespace FoodManager.DTO.Message.Users
{
    public class FindUsersResponse : FindBaseResponse
    {
        public FindUsersResponse()
        {
            Users = new List<UserResponse>();
        }

        public List<UserResponse> Users { get; set; }
    }
}