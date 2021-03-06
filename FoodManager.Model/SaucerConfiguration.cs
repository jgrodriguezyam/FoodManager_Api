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
        public int Amount { get; set; }

        public bool IsActive { get; set; }
    }
}