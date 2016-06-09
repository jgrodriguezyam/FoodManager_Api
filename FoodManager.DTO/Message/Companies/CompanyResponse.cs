using FoodManager.DTO.Message.Regions;

namespace FoodManager.DTO.Message.Companies
{
    public class CompanyResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public RegionResponse Region { get; set; }
        public bool Status { get; set; }
    }
}