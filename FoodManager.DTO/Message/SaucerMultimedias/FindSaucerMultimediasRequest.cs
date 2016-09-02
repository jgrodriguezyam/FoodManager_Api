using FoodManager.DTO.BaseRequest;

namespace FoodManager.DTO.Message.SaucerMultimedias
{
    public class FindSaucerMultimediasRequest : FindStatusRequest
    {
         public int SaucerId { get; set; }
         public string SaucerIds { get; set; }
    }
}