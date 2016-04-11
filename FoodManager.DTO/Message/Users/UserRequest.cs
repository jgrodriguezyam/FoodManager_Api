﻿namespace FoodManager.DTO.Message.Users
{
    public class UserRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int? DealerId { get; set; }
        public int RoleId { get; set; }
    }
}