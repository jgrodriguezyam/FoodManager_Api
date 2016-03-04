using System.Collections.Generic;
using FoodManager.Infrastructure.Enums;

namespace FoodManager.DTO.BaseResponse
{
    public class EnumeratorResponse
    {
        public EnumeratorResponse()
        {
            Enumerator = new List<Enumerator>();
        }

        public List<Enumerator> Enumerator { get; set; }
    }
}