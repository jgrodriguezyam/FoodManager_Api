namespace FoodManager.Infrastructure.Application
{
    public interface INutritionInformation
    {
        decimal Energy { get; set; }
        decimal Protein { get; set; }
        decimal Carbohydrate { get; set; }
        decimal Sugar { get; set; }
        decimal Lipid { get; set; }
        decimal Sodium { get; set; }
    }
}