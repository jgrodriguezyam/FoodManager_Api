using FoodManager.DTO.BaseRequest;

namespace FoodManager.DTO.Message.Warnings
{
    public class FindWarningsRequest : FindStatusRequest
    {
        public int DiseaseId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
    }
}