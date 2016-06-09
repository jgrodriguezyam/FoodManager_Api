using System.Web.Http;
using FoodManager.DTO.BaseRequest;
using FoodManager.DTO.BaseResponse;
using FoodManager.DTO.Message.Tips;
using FoodManager.Services.Interfaces;

namespace FoodManager.Controllers
{
    public class TipController : ApiController
    {
        private readonly ITipService _tipService;

        public TipController(ITipService tipService)
        {
            _tipService = tipService;
        }

        [HttpGet, Route("tips")]
        public FindTipsResponse Get(FindTipsRequest request)
        {
            return _tipService.Find(request);
        }

        [HttpPost, Route("tips")]
        public CreateResponse Post(TipRequest request)
        {
            return _tipService.Create(request);
        }

        [HttpPut, Route("tips")]
        public SuccessResponse Put(TipRequest request)
        {
            return _tipService.Update(request);
        }

        [HttpGet, Route("tips/{Id}")]
        public TipResponse Get(GetTipRequest request)
        {
            return _tipService.Get(request);
        }

        [HttpDelete, Route("tips/{Id}")]
        public SuccessResponse Delete(DeleteTipRequest request)
        {
            return _tipService.Delete(request);
        }

        [HttpPut, Route("tips/{Id}/status/{Status}")]
        public SuccessResponse ChangeStatus(ChangeStatusRequest request)
        {
            return _tipService.ChangeStatus(request);
        }
    }
}