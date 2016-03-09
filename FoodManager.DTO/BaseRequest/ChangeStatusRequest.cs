namespace FoodManager.DTO.BaseRequest
{
    public class ChangeStatusRequest : IdentifierBaseRequest
    {
         public bool Status { get; set; }
    }
}