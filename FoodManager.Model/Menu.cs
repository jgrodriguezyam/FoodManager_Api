using System;
using FoodManager.Model.Base;
using ServiceStack.DataAnnotations;

namespace FoodManager.Model
{
    public class Menu : EntityBase, IDeletable
    {
        [AutoIncrement]
        public int Id { get; set; }
        public string Comment { get; set; }
        public int DayWeek { get; set; }
        public int MealType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int MaxAmount { get; set; }
        public int DealerId { get; set; }
        public int SaucerId { get; set; }

        public bool IsActive { get; set; }
    }
}