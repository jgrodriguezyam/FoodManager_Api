namespace FoodManager.DTO
{
    public class SaucerConfiguration
    {
        public int Id { get; set; }
        public Saucer Saucer { get; set; }
        public Ingredient Ingredient { get; set; }
        public decimal Portion { get; set; }
        public bool Status { get; set; }
    }
}