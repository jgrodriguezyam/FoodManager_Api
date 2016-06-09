using System.Collections.Generic;
using System.Linq;
using FastMapper;
using FoodManager.DTO.Message.Diseases;
using FoodManager.DTO.Message.Warnings;
using FoodManager.Model;
using FoodManager.Model.IRepositories;
using FoodManager.Services.Factories.Interfaces;

namespace FoodManager.Services.Factories.Implements
{
    public class WarningFactory : IWarningFactory
    {
        private readonly IDiseaseRepository _diseaseRepository;

        public WarningFactory(IDiseaseRepository diseaseRepository)
        {
            _diseaseRepository = diseaseRepository;
        }

        public WarningResponse Execute(Warning warning)
        {
            var warningResponse = TypeAdapter.Adapt<WarningResponse>(warning);
            var disease = _diseaseRepository.FindBy(warning.DiseaseId);
            warningResponse.Disease = TypeAdapter.Adapt<DiseaseResponse>(disease);
            return warningResponse;
        }

        public IEnumerable<WarningResponse> Execute(IEnumerable<Warning> warnings)
        {
            return warnings.Select(Execute);
        }
    }
}