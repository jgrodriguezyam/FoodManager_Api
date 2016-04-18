using System.Collections.Generic;

namespace FoodManager.DTO.Message.AccessLevels
{
    public class FindAccessLevelsResponse
    {
        public FindAccessLevelsResponse()
        {
            AccessLevels = new List<AccessLevelResponse>();
        }

        public List<AccessLevelResponse> AccessLevels { get; set; }
    }
}