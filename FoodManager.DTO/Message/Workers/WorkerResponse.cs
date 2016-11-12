using FoodManager.DTO.Message.Branches;
using FoodManager.DTO.Message.Departments;
using FoodManager.DTO.Message.Jobs;
using FoodManager.DTO.Message.Roles;

namespace FoodManager.DTO.Message.Workers
{
    public class WorkerResponse
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Imss { get; set; }
        public int Gender { get; set; }
        public string Badge { get; set; }
        public int LimitEnergy { get; set; }
        public DepartmentResponse Department { get; set; }
        public JobResponse Job { get; set; }
        public RoleResponse Role { get; set; }
        public BranchResponse Branch { get; set; }
        public bool Status { get; set; }
        public bool IsReference { get; set; }
    }
}