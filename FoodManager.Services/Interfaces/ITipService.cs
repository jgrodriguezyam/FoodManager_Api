using FoodManager.DTO.BaseRequest;
using FoodManager.DTO.BaseResponse;
using FoodManager.DTO.Message.Tips;
using FoodManager.Infrastructure.Files;

namespace FoodManager.Services.Interfaces
{
    public interface ITipService
    {
        FindTipsResponse Find(FindTipsRequest request);
        CreateResponse Create(TipRequest request);
        SuccessResponse Update(TipRequest request);
        TipResponse Get(GetTipRequest request);
        SuccessResponse Delete(DeleteTipRequest request);
        SuccessResponse ChangeStatus(ChangeStatusRequest request);
        SuccessResponse Csv(CsvRequest request, File file);
    }
}