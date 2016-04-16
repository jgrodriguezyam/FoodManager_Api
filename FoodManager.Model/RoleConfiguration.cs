using FoodManager.Model.Base;
using ServiceStack.DataAnnotations;

namespace FoodManager.Model
{
    public class RoleConfiguration : EntityBase, IDeletable
    {
        [AutoIncrement]
        public int Id { get; set; }
        public int RoleId { get; set; }
        public int PermissionId { get; set; }
        public int AccessLevelId { get; set; }

        public bool IsActive { get; set; }
    }
}