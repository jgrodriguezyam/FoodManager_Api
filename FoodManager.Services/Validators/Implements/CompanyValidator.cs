using FluentValidation;
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
            });
        }
    }
}