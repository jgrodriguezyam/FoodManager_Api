using FluentValidation;
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
            });
        }
    }
}