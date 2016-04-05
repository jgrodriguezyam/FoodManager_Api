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
        public decimal Energy { get; set; }
        public decimal Protein { get; set; }
        public decimal Carbohydrate { get; set; }
        public decimal Sugar { get; set; }
        public decimal Lipid { get; set; }
        public decimal Sodium { get; set; }
        public int WorkerId { get; set; }
        public int SaucerId { get; set; }

        public bool IsActive { get; set; }
    }
}