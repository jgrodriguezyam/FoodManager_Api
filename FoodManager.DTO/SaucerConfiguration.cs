﻿namespace FoodManager.DTO
{
    public class SaucerConfiguration
    {
        public int Id { get; set; }
        public int SaucerId { get; set; }
        public int IngredientId { get; set; }
        public int Amount { get; set; }
        public bool Status { get; set; }
    }
}