using System.Collections.Generic;
using FoodManager.DTO.BaseResponse;

namespace FoodManager.DTO.Message.Diseases
{
    public class FindDiseasesResponse : FindBaseResponse
    {
        public FindDiseasesResponse()
        {
            Diseases = new List<DiseaseResponse>();
        }

        public List<DiseaseResponse> Diseases { get; set; } 
    }
}