using FoodManager.Infrastructure.Application;
using FoodManager.Model.Base;
using ServiceStack.DataAnnotations;

namespace FoodManager.Model
{
    public class Ingredient : EntityBase, IDeletable, INutritionInformation
    {
        [AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Energy { get; set; }
        public decimal Protein { get; set; }
        public decimal Carbohydrate { get; set; }
        public decimal Sugar { get; set; }
        public decimal Lipid { get; set; }
        public decimal Sodium { get; set; }
        public decimal NetWeight { get; set; }
        public int Unit { get; set; }
        public int IngredientGroupId { get; set; }

        public bool IsActive { get; set; }
    }
}