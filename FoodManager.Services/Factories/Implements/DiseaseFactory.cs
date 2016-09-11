using System.Collections.Generic;
using System.Linq;
using FastMapper;
using FoodManager.DTO.Message.Diseases;
using FoodManager.Infrastructure.Integers;
using FoodManager.Model;
using FoodManager.Model.IRepositories;
using FoodManager.Services.Factories.Interfaces;

namespace FoodManager.Services.Factories.Implements
{
    public class DiseaseFactory : IDiseaseFactory
    {
        private readonly IWarningRepository _warningRepository;

        public DiseaseFactory(IWarningRepository warningRepository)
        {
            _warningRepository = warningRepository;
        }

        public DiseaseResponse Execute(Disease disease)
        {
            return AppendProperties(new[] { disease }).FirstOrDefault();
        }

        public IEnumerable<DiseaseResponse> Execute(IEnumerable<Disease> diseases)
        {
            return AppendProperties(diseases);
        }

        private IEnumerable<DiseaseResponse> AppendProperties(IEnumerable<Disease> diseases)
        {
            var diseasesResponse = TypeAdapter.Adapt<List<DiseaseResponse>>(diseases);
            var warnings = _warningRepository.FindBy(warning => warning.IsActive);

            diseasesResponse.ForEach(diseaseResponse =>
            {
                var disease = diseases.First(diseaseModel => diseaseModel.Id == diseaseResponse.Id);
                var amountOfReferences = warnings.Count(warning => warning.DiseaseId == disease.Id);
                diseaseResponse.IsReference = amountOfReferences.IsNotZero();
            });

            return diseasesResponse;
        }
    }
}