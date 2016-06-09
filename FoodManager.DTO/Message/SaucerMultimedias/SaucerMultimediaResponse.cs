using FoodManager.DTO.Message.Saucers;

namespace FoodManager.DTO.Message.SaucerMultimedias
{
    public class SaucerMultimediaResponse
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public SaucerResponse Saucer { get; set; }
        public bool Status { get; set; }
    }
}