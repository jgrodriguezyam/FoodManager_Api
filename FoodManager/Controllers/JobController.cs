using System.Web.Http;
using FoodManager.DTO.BaseRequest;
using FoodManager.DTO.BaseResponse;
using FoodManager.DTO.Message.Jobs;
using FoodManager.Services.Interfaces;

namespace FoodManager.Controllers
{
    public class JobController : ApiController
    {
        private readonly IJobService _jobService;

        public JobController(IJobService jobService)
        {
            _jobService = jobService;
        }

        [HttpGet, Route("jobs")]
        public FindJobsResponse Get(FindJobsRequest request)
        {
            return _jobService.Find(request);
        }

        [HttpPost, Route("jobs")]
        public CreateResponse Post(JobRequest request)
        {
            return _jobService.Create(request);
        }

        [HttpPut, Route("jobs")]
        public SuccessResponse Put(JobRequest request)
        {
            return _jobService.Update(request);
        }

        [HttpGet, Route("jobs/{Id}")]
        public JobResponse Get(GetJobRequest request)
        {
            return _jobService.Get(request);
        }

        [HttpDelete, Route("jobs/{Id}")]
        public SuccessResponse Delete(DeleteJobRequest request)
        {
            return _jobService.Delete(request);
        }

        [HttpPut, Route("jobs/{Id}/status/{Status}")]
        public SuccessResponse ChangeStatus(ChangeStatusRequest request)
        {
            return _jobService.ChangeStatus(request);
        }

        [HttpGet, Route("jobs/is-reference/{Id}")]
        public SuccessResponse Get(IsReferenceRequest request)
        {
            return _jobService.IsReference(request);
        }
    }
}