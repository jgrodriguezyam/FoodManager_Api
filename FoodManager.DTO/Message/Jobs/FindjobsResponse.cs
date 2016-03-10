using System.Collections.Generic;
using FoodManager.DTO.BaseResponse;

namespace FoodManager.DTO.Message.Jobs
{
    public class FindJobsResponse : FindBaseResponse
    {
        public FindJobsResponse()
        {
            Jobs = new List<JobResponse>();
        }

        public List<JobResponse> Jobs { get; set; } 
    }
}