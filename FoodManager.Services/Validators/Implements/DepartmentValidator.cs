using FluentValidation;
using FoodManager.Infrastructure.Integers;
using FoodManager.Infrastructure.Validators;
using FoodManager.Model;
using FoodManager.Services.Validators.Interfaces;

namespace FoodManager.Services.Validators.Implements
{
    public class DepartmentValidator : BaseValidator<Department>, IDepartmentValidator
    {
        public DepartmentValidator()
        {
            RuleSet("Base", () =>
            {
                RuleFor(department => department.Name).NotNull().NotEmpty();
                RuleFor(department => department.BranchId).Must(branchId => branchId.IsNotZero()).WithMessage("Tienes que elegir una sucursal");
            });
        }
    }
}