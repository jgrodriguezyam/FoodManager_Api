using FluentValidation;
using FoodManager.Infrastructure.Integers;
using FoodManager.Infrastructure.Validators;
using FoodManager.Model;
using FoodManager.Services.Validators.Interfaces;

namespace FoodManager.Services.Validators.Implements
{
    public class CompanyValidator : BaseValidator<Company>, ICompanyValidator
    {
        public CompanyValidator()
        {
            RuleSet("Base", () =>
            {
                RuleFor(company => company.Name).NotNull().NotEmpty();
                RuleFor(company => company.RegionId).Must(regionId => regionId.IsNotZero()).WithMessage("Tienes que elegir una region");
            });
        }
    }
}