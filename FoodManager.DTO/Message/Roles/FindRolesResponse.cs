using System.Collections.Generic;
using FoodManager.DTO.BaseResponse;

namespace FoodManager.DTO.Message.Roles
{
    public class FindRolesResponse : FindBaseResponse
    {
        public FindRolesResponse()
        {
            Roles = new List<RoleResponse>();
        }

        public List<RoleResponse> Roles { get; set; } 
    }
}