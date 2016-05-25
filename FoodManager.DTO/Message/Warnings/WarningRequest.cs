namespace FoodManager.DTO.Message.Warnings
{
    public class WarningRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int DiseaseId { get; set; }
    }
}