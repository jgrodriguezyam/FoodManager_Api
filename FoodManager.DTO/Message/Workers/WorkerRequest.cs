namespace FoodManager.DTO.Message.Workers
{
    public class WorkerRequest
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
        public int DepartmentId { get; set; }
        public int JobId { get; set; }
        public int RoleId { get; set; }
        public int BranchId { get; set; }
    }
}