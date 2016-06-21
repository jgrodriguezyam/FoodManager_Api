using System.Collections.Generic;
using FoodManager.DTO.Message.Diseases;
using FoodManager.Model;

namespace FoodManager.Services.Factories.Interfaces
{
    public interface IDiseaseFactory
    {
        DiseaseResponse Execute(Disease disease);
        IEnumerable<DiseaseResponse> Execute(IEnumerable<Disease> diseases);
    }
}