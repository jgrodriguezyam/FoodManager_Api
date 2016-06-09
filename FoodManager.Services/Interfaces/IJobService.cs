using FoodManager.DTO.BaseRequest;
using FoodManager.DTO.BaseResponse;
using FoodManager.DTO.Message.Jobs;

namespace FoodManager.Services.Interfaces
{
    public interface IJobService
    {
        FindJobsResponse Find(FindJobsRequest request);
        CreateResponse Create(JobRequest request);
        SuccessResponse Update(JobRequest request);
        JobResponse Get(GetJobRequest request);
        SuccessResponse Delete(DeleteJobRequest request);
        SuccessResponse ChangeStatus(ChangeStatusRequest request);
        SuccessResponse IsReference(IsReferenceRequest request);
    }
}