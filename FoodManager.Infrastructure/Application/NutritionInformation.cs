namespace FoodManager.Infrastructure.Application
{
    public class NutritionInformation : INutritionInformation
    {
        public decimal Energy { get; set; }
        public decimal Protein { get; set; }
        public decimal Carbohydrate { get; set; }
        public decimal Sugar { get; set; }
        public decimal Lipid { get; set; }
        public decimal Sodium { get; set; }
    }
}