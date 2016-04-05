using System;
using FoodManager.Infrastructure.Application;
using FoodManager.Model.Base;
using ServiceStack.DataAnnotations;

namespace FoodManager.Model
{
    public class Reservation : EntityBase, IDeletable, INutritionInformation
    {
        [AutoIncrement]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int KiloCalorie { get; set; }
        public int Protein { get; set; }
        public int Lipid { get; set; }
        public int Hdec { get; set; }
        public int Sodium { get; set; }
        public int WorkerId { get; set; }
        public int SaucerId { get; set; }

        public bool IsActive { get; set; }
    }
}