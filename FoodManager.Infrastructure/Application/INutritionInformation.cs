namespace FoodManager.Infrastructure.Application
{
    public interface INutritionInformation
    {
         int KiloCalorie { get; set; }
         int Protein { get; set; }
         int Lipid { get; set; }
         int Hdec { get; set; }
         int Sodium { get; set; }
    }
}