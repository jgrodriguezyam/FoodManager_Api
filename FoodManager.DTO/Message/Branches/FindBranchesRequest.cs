﻿using FoodManager.DTO.BaseRequest;

namespace FoodManager.DTO.Message.Branches
{
    public class FindBranchesRequest : FindStatusRequest
    {
         public int CompanyId { get; set; }
    }
}