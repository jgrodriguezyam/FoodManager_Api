using System.Web.Http;
using FoodManager.DTO.BaseRequest;
using FoodManager.DTO.BaseResponse;
using FoodManager.DTO.Message.Diseases;
using FoodManager.Services.Interfaces;

namespace FoodManager.Controllers
{
    public class DiseaseController : ApiController
    {
        private readonly IDiseaseService _diseaseService;

        public DiseaseController(IDiseaseService diseaseService)
        {
            _diseaseService = diseaseService;
        }

        [HttpGet, Route("diseases")]
        public FindDiseasesResponse Get(FindDiseasesRequest request)
        {
            return _diseaseService.Find(request);
        }

        [HttpPost, Route("diseases")]
        public CreateResponse Post(DiseaseRequest request)
        {
            return _diseaseService.Create(request);
        }

        [HttpPut, Route("diseases")]
        public SuccessResponse Put(DiseaseRequest request)
        {
            return _diseaseService.Update(request);
        }

        [HttpGet, Route("diseases/{Id}")]
        public DiseaseResponse Get(GetDiseaseRequest request)
        {
            return _diseaseService.Get(request);
        }

        [HttpDelete, Route("diseases/{Id}")]
        public SuccessResponse Delete(DeleteDiseaseRequest request)
        {
            return _diseaseService.Delete(request);
        }

        [HttpPut, Route("diseases/{Id}/status/{Status}")]
        public SuccessResponse ChangeStatus(ChangeStatusRequest request)
        {
            return _diseaseService.ChangeStatus(request);
        }

        [HttpGet, Route("diseases/is-reference/{Id}")]
        public SuccessResponse Get(IsReferenceRequest request)
        {
            return _diseaseService.IsReference(request);
        }
    }
}