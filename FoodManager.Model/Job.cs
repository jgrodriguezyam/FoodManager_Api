using FoodManager.Model.Base;
using ServiceStack.DataAnnotations;

namespace FoodManager.Model
{
    public class Job : EntityBase, IDeletable
    {
        [AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }

        public bool IsActive { get; set; }
    }
}