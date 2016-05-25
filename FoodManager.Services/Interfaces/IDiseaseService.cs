using FoodManager.DTO;
using FoodManager.DTO.BaseRequest;
using FoodManager.DTO.BaseResponse;
using FoodManager.DTO.Message.Diseases;

namespace FoodManager.Services.Interfaces
{
    public interface IDiseaseService
    {
        FindDiseasesResponse Find(FindDiseasesRequest request);
        CreateResponse Create(DiseaseRequest request);
        SuccessResponse Update(DiseaseRequest request);
        Disease Get(GetDiseaseRequest request);
        SuccessResponse Delete(DeleteDiseaseRequest request);
        SuccessResponse ChangeStatus(ChangeStatusRequest request);
        SuccessResponse IsReference(IsReferenceRequest request);
    }
}