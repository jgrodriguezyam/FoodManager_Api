namespace FoodManager.DTO.BaseResponse
{
    public class CreateResponse
    {
        public CreateResponse(int id)
        {
            Id = id;
        }

        public int Id { get; private set; }
    }
}