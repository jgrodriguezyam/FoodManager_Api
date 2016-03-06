using FluentValidation;
using FoodManager.Infrastructure.Integers;
using FoodManager.Infrastructure.Validators;
using FoodManager.Model;
using FoodManager.Services.Validators.Interfaces;

namespace FoodManager.Services.Validators.Implements
{
    public class BranchValidator : BaseValidator<Branch>, IBranchValidator
    {
        public BranchValidator()
        {
            RuleSet("Base", () =>
            {
                RuleFor(branch => branch.Name).NotNull().NotEmpty();
                RuleFor(branch => branch.Code).NotNull().NotEmpty();
                RuleFor(branch => branch.CompanyId).Must(companyId => companyId.IsNotZero()).WithMessage("Tienes que elegir una compania");
            });
        }
    }
}