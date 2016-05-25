using System.Collections.Generic;
using FoodManager.DTO.BaseResponse;

namespace FoodManager.DTO.Message.Companies
{
    public class FindCompaniesResponse : FindBaseResponse
    {
        public FindCompaniesResponse()
        {
            Companies = new List<CompanyResponse>();
        }

        public List<CompanyResponse> Companies { get; set; } 
    }
}