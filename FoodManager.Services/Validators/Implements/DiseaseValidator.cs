using FluentValidation;
using FoodManager.Infrastructure.Validators;
using FoodManager.Model;
using FoodManager.Services.Validators.Interfaces;

namespace FoodManager.Services.Validators.Implements
{
    public class DiseaseValidator : BaseValidator<Disease>, IDiseaseValidator
    {
        public DiseaseValidator()
        {
            RuleSet("Base", () =>
            {
                RuleFor(disease => disease.Name).NotNull().NotEmpty();
                RuleFor(disease => disease.Code).NotNull().NotEmpty();
            });
        }
    }
}