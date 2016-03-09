using FoodManager.Model.Base;
using ServiceStack.DataAnnotations;

namespace FoodManager.Model
{
    public class Warning : EntityBase, IDeletable
    {
        [AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int DiseaseId { get; set; }

        public bool IsActive { get; set; }
    }
}