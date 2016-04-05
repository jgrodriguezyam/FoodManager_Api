using FluentValidation;
using FluentValidation.Results;
using FoodManager.Infrastructure.Collections;
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
        private readonly IWarningRepository _warningRepository;

        public WarningValidator(IDiseaseRepository diseaseRepository, IWarningRepository warningRepository)
        {
            _diseaseRepository = diseaseRepository;
            _warningRepository = warningRepository;

            RuleSet("Base", () =>
            {
                RuleFor(warning => warning.Name).NotNull().NotEmpty();
                RuleFor(warning => warning.Code).NotNull().NotEmpty();
                RuleFor(warning => warning.DiseaseId).Must(diseaseId => diseaseId.IsNotZero()).WithMessage("Tienes que elegir una enfermedad");
                Custom(ReferencesValidate);
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

        public ValidationFailure ReferencesValidate(Warning warning, ValidationContext<Warning> context)
        {
            var disease = _diseaseRepository.FindBy(warning.DiseaseId);
            if (disease.IsNull() || disease.Status.Equals(GlobalConstants.StatusDeactivated))
                return new ValidationFailure("Warning", "La enfermedad esta desactivada o no existe");
            
            return null;
        }

        public ValidationFailure CreateCodeValidate(Warning warning, ValidationContext<Warning> context)
        {
            var warningsRetrieved = _warningRepository.FindBy(bran => bran.Code == warning.Code && bran.IsActive);
            if (warningsRetrieved.IsNotEmpty())
                return new ValidationFailure("Warning", "Ya existe codigo");

            return null;
        }

        public ValidationFailure UpdateCodeValidate(Warning warning, ValidationContext<Warning> context)
        {
            var warningsRetrieved = _warningRepository.FindBy(bran => bran.Code == warning.Code && bran.Id != warning.Id && bran.IsActive);
            if (warningsRetrieved.IsNotEmpty())
                return new ValidationFailure("Warning", "Ya existe codigo");

            return null;
        }
    }
}