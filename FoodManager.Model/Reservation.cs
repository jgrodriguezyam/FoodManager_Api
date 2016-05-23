using System;
using FoodManager.Model.Base;
using ServiceStack.DataAnnotations;

namespace FoodManager.Model
{
    public class Reservation : EntityBase, IDeletable
    {
        [AutoIncrement]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public decimal Portion { get; set; }
        public int WorkerId { get; set; }
        public int SaucerId { get; set; }
        public int DealerId { get; set; }

        public bool IsActive { get; set; }
    }
}