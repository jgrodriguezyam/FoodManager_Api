using FluentValidation;
using FluentValidation.Results;
using FoodManager.Infrastructure.Collections;
using FoodManager.Infrastructure.Validators;
using FoodManager.Model;
using FoodManager.Model.IRepositories;
using FoodManager.Services.Validators.Interfaces;

namespace FoodManager.Services.Validators.Implements
{
    public class DiseaseValidator : BaseValidator<Disease>, IDiseaseValidator
    {
        private readonly IDiseaseRepository _diseaseRepository;

        public DiseaseValidator(IDiseaseRepository diseaseRepository)
        {
            _diseaseRepository = diseaseRepository;

            RuleSet("Base", () =>
            {
                RuleFor(disease => disease.Name).NotNull().NotEmpty();
                RuleFor(disease => disease.Code).NotNull().NotEmpty();
            });

            RuleSet("Create", () =>
            {
                Custom(CreateCodeValidate);
            });

            RuleSet("Update", () =>
            {
                Custom(UpdateCodeValidate);
            });
        }

        public ValidationFailure CreateCodeValidate(Disease disease, ValidationContext<Disease> context)
        {
            var diseasesRetrieved = _diseaseRepository.FindBy(dise => dise.Code == disease.Code && dise.IsActive);
            if (diseasesRetrieved.IsNotEmpty())
                return new ValidationFailure("Disease", "Ya existe codigo");

            return null;
        }

        public ValidationFailure UpdateCodeValidate(Disease disease, ValidationContext<Disease> context)
        {
            var diseasesRetrieved = _diseaseRepository.FindBy(dise => dise.Code == disease.Code && dise.Id != disease.Id && dise.IsActive);
            if (diseasesRetrieved.IsNotEmpty())
                return new ValidationFailure("Disease", "Ya existe codigo");

            return null;
        }
    }
}