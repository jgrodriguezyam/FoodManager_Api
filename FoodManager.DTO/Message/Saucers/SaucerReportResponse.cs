using System.Collections.Generic;

namespace FoodManager.DTO.Message.Saucers
{
    public class SaucerReportResponse
    {
        public List<SaucerTopReportResponse> Main { get; set; }
        public List<SaucerTopReportResponse> Garrison { get; set; }
    }
}