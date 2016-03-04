namespace FoodManager.DTO.BaseRequest
{
    public class FindBaseRequest
    {
        public string Sort { get; set; }
        public string SortBy { get; set; }
        public int StartPage { get; set; }
        public int EndPage { get; set; }
    }
}