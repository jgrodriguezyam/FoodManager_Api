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
        public int DepartmentId { get; set; }
        public int JobId { get; set; }
        public int DealerId { get; set; }
        public bool Status { get; set; }
    }
}