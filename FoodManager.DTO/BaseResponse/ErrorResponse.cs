namespace FoodManager.DTO.BaseResponse
{
    public class ErrorResponse
    {
        public ErrorResponse(string message)
        {
            Message = message;
        }

        public string Message { get; private set; }
    }
}