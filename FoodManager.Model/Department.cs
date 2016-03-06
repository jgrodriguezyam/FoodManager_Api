using FoodManager.Model.Base;
using ServiceStack.DataAnnotations;

namespace FoodManager.Model
{
    public class Department : EntityBase, IDeletable
    {
        [AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public int BranchId { get; set; }

        public bool IsActive { get; set; }
    }
}