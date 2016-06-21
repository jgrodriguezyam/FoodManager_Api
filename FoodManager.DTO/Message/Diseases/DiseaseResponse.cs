﻿namespace FoodManager.DTO.Message.Diseases
{
    public class DiseaseResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public bool Status { get; set; }
        public bool IsReference { get; set; }
    }
}