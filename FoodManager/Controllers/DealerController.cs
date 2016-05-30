using System.Web.Http;
using FoodManager.DTO;
using FoodManager.DTO.BaseRequest;
using FoodManager.DTO.BaseResponse;
using FoodManager.DTO.Message.Dealers;
using FoodManager.Services.Interfaces;

namespace FoodManager.Controllers
{
    public class DealerController : ApiController
    {
        private readonly IDealerService _dealerService;

        public DealerController(IDealerService dealerService)
        {
            _dealerService = dealerService;
        }

        [HttpGet, Route("dealers")]
        public FindDealersResponse Get(FindDealersRequest request)
        {
            return _dealerService.Find(request);
        }

        [HttpPost, Route("dealers")]
        public CreateResponse Post(DealerRequest request)
        {
            return _dealerService.Create(request);
        }

        [HttpPut, Route("dealers")]
        public SuccessResponse Put(DealerRequest request)
        {
            return _dealerService.Update(request);
        }

        [HttpGet, Route("dealers/{Id}")]
        public Dealer Get(GetDealerRequest request)
        {
            return _dealerService.Get(request);
        }

        [HttpDelete, Route("dealers/{Id}")]
        public SuccessResponse Delete(DeleteDealerRequest request)
        {
            return _dealerService.Delete(request);
        }

        [HttpPut, Route("dealers/{Id}/status/{Status}")]
        public SuccessResponse ChangeStatus(ChangeStatusRequest request)
        {
            return _dealerService.ChangeStatus(request);
        }

        [HttpGet, Route("dealers/is-reference/{Id}")]
        public SuccessResponse Get(IsReferenceRequest request)
        {
            return _dealerService.IsReference(request);
        }

        [HttpPost, Route("dealers/{FirstReference}/saucers/{SecondReference}")]
        public SuccessResponse Assign(RelationRequest request)
        {
            return _dealerService.AddSaucer(request);
        }

        [HttpDelete, Route("dealers/{FirstReference}/saucers/{SecondReference}")]
        public SuccessResponse Unassign(RelationRequest request)
        {
            return _dealerService.RemoveSaucer(request);
        }
    }
}