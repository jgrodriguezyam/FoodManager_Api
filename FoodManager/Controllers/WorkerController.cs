using System.Web.Http;
using FoodManager.DTO;
using FoodManager.DTO.BaseRequest;
using FoodManager.DTO.BaseResponse;
using FoodManager.DTO.Message.Workers;
using FoodManager.Services.Interfaces;

namespace FoodManager.Controllers
{
    public class WorkerController : ApiController
    {
        private readonly IWorkerService _workerService;

        public WorkerController(IWorkerService workerService)
        {
            _workerService = workerService;
        }

        [HttpGet, Route("workers")]
        public FindWorkersResponse Get(FindWorkersRequest request)
        {
            return _workerService.Find(request);
        }

        [HttpPost, Route("workers")]
        public CreateResponse Post(WorkerRequest request)
        {
            return _workerService.Create(request);
        }

        [HttpPut, Route("workers")]
        public SuccessResponse Put(WorkerRequest request)
        {
            return _workerService.Update(request);
        }

        [HttpGet, Route("workers/{Id}")]
        public Worker Get(GetWorkerRequest request)
        {
            return _workerService.Get(request);
        }

        [HttpDelete, Route("workers/{Id}")]
        public SuccessResponse Delete(DeleteWorkerRequest request)
        {
            return _workerService.Delete(request);
        }

        [HttpPost, Route("workers/login")]
        public LoginWorkerResponse Login(LoginWorkerRequest request)
        {
            return _workerService.Login(request);
        }

        [HttpPost, Route("workers/logout/{Id}")]
        public SuccessResponse Logout(LogoutWorkerRequest request)
        {
            return _workerService.Logout(request);
        }

        [HttpPut, Route("workers/{Id}/status/{Status}")]
        public SuccessResponse Put(ChangeStatusRequest request)
        {
            return _workerService.ChangeStatus(request);
        }
    }
}