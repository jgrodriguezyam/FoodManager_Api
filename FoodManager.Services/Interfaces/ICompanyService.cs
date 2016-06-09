using FoodManager.DTO.BaseRequest;
using FoodManager.DTO.BaseResponse;
using FoodManager.DTO.Message.Companies;

namespace FoodManager.Services.Interfaces
{
    public interface ICompanyService
    {
        FindCompaniesResponse Find(FindCompaniesRequest request);
        CreateResponse Create(CompanyRequest request);
        SuccessResponse Update(CompanyRequest request);
        CompanyResponse Get(GetCompanyRequest request);
        SuccessResponse Delete(DeleteCompanyRequest request);
        SuccessResponse ChangeStatus(ChangeStatusRequest request);
        SuccessResponse IsReference(IsReferenceRequest request);
    }
}