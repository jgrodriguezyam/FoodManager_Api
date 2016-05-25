namespace FoodManager.DTO.Message.Warnings
{
    public class WarningResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int DiseaseId { get; set; }
        public bool Status { get; set; }
    }
}