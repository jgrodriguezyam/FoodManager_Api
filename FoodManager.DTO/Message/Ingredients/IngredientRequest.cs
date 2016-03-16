namespace FoodManager.DTO.Message.Ingredients
{
    public class IngredientRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Amount { get; set; }
        public int KiloCalorie { get; set; }
        public int Protein { get; set; }
        public int Lipid { get; set; }
        public int Hdec { get; set; }
        public int Sodium { get; set; }
        public int IngredientGroupId { get; set; }
    }
}