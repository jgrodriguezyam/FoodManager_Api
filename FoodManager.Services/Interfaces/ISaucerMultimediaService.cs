using System.IO;
using FoodManager.DTO.BaseRequest;
using FoodManager.DTO.BaseResponse;
using FoodManager.DTO.Message.SaucerMultimedias;
using File = FoodManager.Infrastructure.Files.File;

namespace FoodManager.Services.Interfaces
{
    public interface ISaucerMultimediaService
    {
        FindSaucerMultimediasResponse Find(FindSaucerMultimediasRequest request);
        CreateResponse Create(SaucerMultimediaRequest request, File file);
        SuccessResponse Update(SaucerMultimediaRequest request);
        SaucerMultimediaResponse Get(GetSaucerMultimediaRequest request);
        SuccessResponse Delete(DeleteSaucerMultimediaRequest request);
        SuccessResponse ChangeStatus(ChangeStatusRequest request);
        Stream GetFile(GetFileRequest request);
        SuccessResponse DeleteByParent(DeleteByParentRequest request);
    }
}