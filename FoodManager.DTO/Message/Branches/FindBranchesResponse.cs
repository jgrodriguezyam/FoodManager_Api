using System.Collections.Generic;
using FoodManager.DTO.BaseResponse;

namespace FoodManager.DTO.Message.Branches
{
    public class FindBranchesResponse : FindBaseResponse
    {
        public FindBranchesResponse()
        {
            Branches = new List<BranchResponse>();
        }

        public List<BranchResponse> Branches { get; set; } 
    }
}