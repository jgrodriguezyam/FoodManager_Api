﻿namespace FoodManager.DTO.Message.RoleConfigurations
{
    public class RoleConfigurationRequest
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public int PermissionId { get; set; }
        public int AccessLevelId { get; set; }
    }
}