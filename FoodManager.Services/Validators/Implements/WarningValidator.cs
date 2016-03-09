using FluentValidation;
using FoodManager.Infrastructure.Integers;
using FoodManager.Infrastructure.Validators;
using FoodManager.Model;
using FoodManager.Services.Validators.Interfaces;

namespace FoodManager.Services.Validators.Implements
{
    public class WarningValidator : BaseValidator<Warning>, IWarningValidator
    {
        public WarningValidator()
        {
            RuleSet("Base", () =>
            {
                RuleFor(warning => warning.Name).NotNull().NotEmpty();
                RuleFor(warning => warning.Code).NotNull().NotEmpty();
                RuleFor(warning => warning.DiseaseId).Must(diseaseId => diseaseId.IsNotZero()).WithMessage("Tienes que elegir una enfermedad");
            });
        }
    }
}