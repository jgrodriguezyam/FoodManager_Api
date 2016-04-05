namespace FoodManager.Infrastructure.Application
{
    public class NutritionInformation : INutritionInformation
    {
        public int KiloCalorie { get; set; }
        public int Protein { get; set; }
        public int Lipid { get; set; }
        public int Hdec { get; set; }
        public int Sodium { get; set; }
    }
}