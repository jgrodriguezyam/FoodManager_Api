using System.Collections.Generic;
using FoodManager.DTO.BaseResponse;

namespace FoodManager.DTO.Message.Departments
{
    public class FindDepartmentsResponse : FindBaseResponse
    {
        public FindDepartmentsResponse()
        {
            Departments = new List<DepartmentResponse>();
        }

        public List<DepartmentResponse> Departments { get; set; } 
    }
}