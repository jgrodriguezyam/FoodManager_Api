using FluentValidation;
using FoodManager.Infrastructure.Integers;
using FoodManager.Infrastructure.Validators;
using FoodManager.Model;
using FoodManager.Services.Validators.Interfaces;

namespace FoodManager.Services.Validators.Implements
{
    public class UserValidator : BaseValidator<User>, IUserValidator
    {
        public UserValidator()
        {
            RuleSet("Base", () =>
                            {
                                RuleFor(user => user.Name).NotNull().NotEmpty();
                                RuleFor(user => user.Type).Must(type => type.IsNotZero()).WithMessage("Tienes que elegir un tipo");
                                RuleFor(user => user.UserName).NotNull().NotEmpty();
                                RuleFor(user => user.Password).NotNull().NotEmpty();
                            });
        }
    }
}