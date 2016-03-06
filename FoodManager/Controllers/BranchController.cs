using System.Web.Http;
using FoodManager.DTO;
using FoodManager.DTO.BaseResponse;
using FoodManager.DTO.Message.Branches;
using FoodManager.Services.Interfaces;

namespace FoodManager.Controllers
{
    public class BranchController : ApiController
    {
        private readonly IBranchService _branchService;

        public BranchController(IBranchService branchService)
        {
            _branchService = branchService;
        }

        [HttpGet, Route("branches")]
        public FindBranchesResponse Get(FindBranchesRequest request)
        {
            return _branchService.Find(request);
        }

        [HttpPost, Route("branches")]
        public CreateResponse Post(BranchRequest request)
        {
            return _branchService.Create(request);
        }

        [HttpPut, Route("branches")]
        public SuccessResponse Put(BranchRequest request)
        {
            return _branchService.Update(request);
        }

        [HttpGet, Route("branches/{Id}")]
        public Branch Get(GetBranchRequest request)
        {
            return _branchService.Get(request);
        }

        [HttpDelete, Route("branches/{Id}")]
        public SuccessResponse Delete(DeleteBranchRequest request)
        {
            return _branchService.Delete(request);
        }
    }
}