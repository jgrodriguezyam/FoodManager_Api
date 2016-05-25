using ServiceStack.DataAnnotations;
using FoodManager.Model.Base;

namespace FoodManager.Model
{
    public class Region : EntityBase, IDeletable
    {
        [AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        
        public bool IsActive { get; set; }
    }
}