using FoodManager.Model.Base;
using ServiceStack.DataAnnotations;

namespace FoodManager.Model
{
    public class SaucerMultimedia : EntityBase, IDeletable
    {
        [AutoIncrement]
        public int Id { get; set; }
        public string Path { get; set; }
        public int SaucerId { get; set; }

        public bool IsActive { get; set; }
    }
}