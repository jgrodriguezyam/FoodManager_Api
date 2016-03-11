using FluentValidation;
using FluentValidation.Results;
using FoodManager.Infrastructure.Constants;
using FoodManager.Infrastructure.Integers;
using FoodManager.Infrastructure.Objects;
using FoodManager.Infrastructure.Validators;
using FoodManager.Model;
using FoodManager.Model.IRepositories;
using FoodManager.Services.Validators.Interfaces;

namespace FoodManager.Services.Validators.Implements
{
    public class WarningValidator : BaseValidator<Warning>, IWarningValidator
    {
        private readonly IDiseaseRepository _diseaseRepository;

        public WarningValidator(IDiseaseRepository diseaseRepository)
        {
            _diseaseRepository = diseaseRepository;

            RuleSet("Base", () =>
            {
                RuleFor(warning => warning.Name).NotNull().NotEmpty();
                RuleFor(warning => warning.Code).NotNull().NotEmpty();
                RuleFor(warning => warning.DiseaseId).Must(diseaseId => diseaseId.IsNotZero()).WithMessage("Tienes que elegir una enfermedad");
                Custom(ReferencesValidate);
            });
        }

        public ValidationFailure ReferencesValidate(Warning warning, ValidationContext<Warning> context)
        {
            var disease = _diseaseRepository.FindBy(warning.DiseaseId);
            if (disease.IsNull() || disease.Status.Equals(GlobalConstants.StatusDeactivated))
                return new ValidationFailure("Warning", "La enfermedad esta desactivada o no existe");
            
            return null;
        }
    }
}