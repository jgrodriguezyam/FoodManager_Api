using FoodManager.Model.Base;
using ServiceStack.DataAnnotations;

namespace FoodManager.Model
{
    public class Branch : EntityBase, IDeletable
    {
        [AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int RegionId { get; set; }
        public int CompanyId { get; set; }

        public bool IsActive { get; set; }
    }
}