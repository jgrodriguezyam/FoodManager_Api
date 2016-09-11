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
            return AppendProperties(new[] { warning }).FirstOrDefault();
        }

        public IEnumerable<WarningResponse> Execute(IEnumerable<Warning> warnings)
        {
            return AppendProperties(warnings);
        }

        private IEnumerable<WarningResponse> AppendProperties(IEnumerable<Warning> warnings)
        {
            var warningsResponse = TypeAdapter.Adapt<List<WarningResponse>>(warnings);
            var diseases = _diseaseRepository.FindBy(disease => disease.IsActive);

            warningsResponse.ForEach(warningResponse =>
            {
                var warning = warnings.First(warningModel => warningModel.Id == warningResponse.Id);
                var disease = diseases.First(diseaseModel => diseaseModel.Id == warning.DiseaseId);
                warningResponse.Disease = TypeAdapter.Adapt<DiseaseResponse>(disease);
            });

            return warningsResponse;
        }
    }
}