using FoodManager.Model.Base;
using ServiceStack.DataAnnotations;

namespace FoodManager.Model
{
    public class ReservationDetail : EntityBase, IDeletable
    {
        [AutoIncrement]
        public int Id { get; set; }
        public int ReservationId { get; set; }
        public int IngredientId { get; set; }
        public decimal NetWeight { get; set; }

        public bool IsActive { get; set; }
    }
}