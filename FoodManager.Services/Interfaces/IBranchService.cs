using FoodManager.DTO.BaseRequest;
using FoodManager.DTO.BaseResponse;
using FoodManager.DTO.Message.Branches;

namespace FoodManager.Services.Interfaces
{
    public interface IBranchService
    {
        FindBranchesResponse Find(FindBranchesRequest request);
        CreateResponse Create(BranchRequest request);
        SuccessResponse Update(BranchRequest request);
        BranchResponse Get(GetBranchRequest request);
        SuccessResponse Delete(DeleteBranchRequest request);
        SuccessResponse ChangeStatus(ChangeStatusRequest request);
        SuccessResponse IsReference(IsReferenceRequest request);
        SuccessResponse AddDealer(RelationRequest request);
        SuccessResponse RemoveDealer(RelationRequest request);
    }
}