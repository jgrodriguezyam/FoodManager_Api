using System.Collections.Generic;
using FoodManager.DTO.BaseResponse;

namespace FoodManager.DTO.Message.Workers
{
    public class FindWorkersResponse : FindBaseResponse
    {
        public FindWorkersResponse()
        {
            Workers = new List<WorkerResponse>();
        }

        public List<WorkerResponse> Workers { get; set; } 
    }
}