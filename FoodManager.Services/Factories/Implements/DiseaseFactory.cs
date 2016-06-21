using System.Collections.Generic;
using System.Linq;
using FastMapper;
using FoodManager.DTO.Message.Diseases;
using FoodManager.Model;
using FoodManager.Model.IRepositories;
using FoodManager.Services.Factories.Interfaces;

namespace FoodManager.Services.Factories.Implements
{
    public class DiseaseFactory : IDiseaseFactory
    {
        private readonly IDiseaseRepository _diseaseRepository;

        public DiseaseFactory(IDiseaseRepository diseaseRepository)
        {
            _diseaseRepository = diseaseRepository;
        }

        public DiseaseResponse Execute(Disease disease)
        {
            var diseaseResponse = TypeAdapter.Adapt<DiseaseResponse>(disease);
            diseaseResponse.IsReference = _diseaseRepository.IsReference(disease.Id);
            return diseaseResponse;
        }

        public IEnumerable<DiseaseResponse> Execute(IEnumerable<Disease> diseases)
        {
            return diseases.Select(Execute);
        }
    }
}