namespace FoodManager.DTO
{
    public class Warning
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public Disease Disease { get; set; }
        public bool Status { get; set; }
    }
}