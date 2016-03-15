using System.Collections.Generic;
using FoodManager.DTO.BaseResponse;

namespace FoodManager.DTO.Message.SaucerMultimedias
{
    public class FindSaucerMultimediasResponse : FindBaseResponse
    {
        public FindSaucerMultimediasResponse()
        {
            SaucerMultimedias = new List<SaucerMultimediaResponse>();
        }

        public List<SaucerMultimediaResponse> SaucerMultimedias { get; set; } 
    }
}