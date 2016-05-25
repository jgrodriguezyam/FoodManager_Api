using FluentValidation;
using FoodManager.Infrastructure.Constants;
using FoodManager.Infrastructure.Validators;
using FoodManager.Model;
using FoodManager.Services.Validators.Interfaces;

namespace FoodManager.Services.Validators.Implements
{
    public class RoleValidator : BaseValidator<Role>, IRoleValidator
    {
        public RoleValidator()
        {
            RuleSet("Base", () =>
                            {
                                RuleFor(role => role.Name).NotNull().NotEmpty();
                                RuleFor(role => role.Id).Must(id => id != GlobalConstants.AdminRoleId).WithMessage("El rol administrador no puede ser eliminado o editado");
                            });
        }
    }
}