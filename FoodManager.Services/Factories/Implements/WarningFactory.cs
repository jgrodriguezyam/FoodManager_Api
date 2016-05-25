using FastMapper;
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

        public DTO.Warning Execute(Warning warning)
        {
            var warningDto = TypeAdapter.Adapt<DTO.Warning>(warning);
            var disease = _diseaseRepository.FindBy(warning.DiseaseId);
            warningDto.Disease = TypeAdapter.Adapt<DTO.Disease>(disease);
            return warningDto;
        }
    }
}