using System.Net.Http;
using System.Web.Http;
using FoodManager.DTO;
using FoodManager.DTO.BaseRequest;
using FoodManager.DTO.BaseResponse;
using FoodManager.DTO.Message.SaucerMultimedias;
using FoodManager.Helpers;
using FoodManager.Infrastructure.Files;
using FoodManager.Services.Interfaces;

namespace FoodManager.Controllers
{
    public class SaucerMultimediaController : ApiController
    {
        private readonly ISaucerMultimediaService _saucerMultimediaService;

        public SaucerMultimediaController(ISaucerMultimediaService saucerMultimediaService)
        {
            _saucerMultimediaService = saucerMultimediaService;
        }

        [HttpGet, Route("saucer-multimedias")]
        public FindSaucerMultimediasResponse Get(FindSaucerMultimediasRequest request)
        {
            return _saucerMultimediaService.Find(request);
        }

        [HttpPost, Route("saucer-multimedias/saucers/{SaucerId}/file")]
        public CreateResponse Post(SaucerMultimediaRequest request)
        {
            return _saucerMultimediaService.Create(request, Request.GetFile());
        }

        [HttpPut, Route("saucer-multimedias")]
        public SuccessResponse Put(SaucerMultimediaRequest request)
        {
            return _saucerMultimediaService.Update(request);
        }

        [HttpGet, Route("saucer-multimedias/{Id}")]
        public SaucerMultimedia Get(GetSaucerMultimediaRequest request)
        {
            return _saucerMultimediaService.Get(request);
        }

        [HttpDelete, Route("saucer-multimedias/{Id}")]
        public SuccessResponse Delete(DeleteSaucerMultimediaRequest request)
        {
            return _saucerMultimediaService.Delete(request);
        }

        [HttpPut, Route("saucer-multimedias/{Id}/status/{Status}")]
        public SuccessResponse Put(ChangeStatusRequest request)
        {
            return _saucerMultimediaService.ChangeStatus(request);
        }

        [HttpGet, Route("saucer-multimedias/{Id}/file")]
        public HttpResponseMessage Get(GetFileRequest request)
        {
            var stream = _saucerMultimediaService.GetFile(request);
            return StreamReply.ConvertToHttpResponse(stream);
        }
    }
}