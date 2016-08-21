using FoodManager.Model.Base;
using ServiceStack.DataAnnotations;

namespace FoodManager.Model
{
    public class SaucerConfiguration : EntityBase, IDeletable
    {
        [AutoIncrement]
        public int Id { get; set; }
        public int SaucerId { get; set; }
        public int IngredientId { get; set; }
        public decimal NetWeight { get; set; }

        public bool IsActive { get; set; }
    }
}