using FoodManager.DTO.BaseRequest;
using FoodManager.DTO.BaseResponse;
using FoodManager.DTO.Message.Workers;

namespace FoodManager.Services.Interfaces
{
    public interface IWorkerService
    {
        FindWorkersResponse Find(FindWorkersRequest request);
        CreateResponse Create(WorkerRequest request);
        SuccessResponse Update(WorkerRequest request);
        WorkerResponse Get(GetWorkerRequest request);
        SuccessResponse Delete(DeleteWorkerRequest request);
        LoginWorkerResponse Login(LoginWorkerRequest request);
        SuccessResponse Logout(LogoutWorkerRequest request);
        SuccessResponse ChangeStatus(ChangeStatusRequest request);
        SuccessResponse IsReference(IsReferenceRequest request);
        WorkerReportResponse Report(WorkerReportRequest request);
    }
}