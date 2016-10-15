using System.Linq;
using FluentValidation;
using FluentValidation.Results;
using FoodManager.Infrastructure.Constants;
using FoodManager.Infrastructure.Enums;
using FoodManager.Infrastructure.Objects;
using FoodManager.Infrastructure.Validators;
using FoodManager.Model;
using FoodManager.Model.Enums;
using FoodManager.Model.IRepositories;
using FoodManager.Services.Validators.Interfaces;

namespace FoodManager.Services.Validators.Implements
{
    public class UserValidator : BaseValidator<User>, IUserValidator
    {
        private readonly IUserRepository _userRepository;

        public UserValidator(IUserRepository userRepository)
        {
            _userRepository = userRepository;

            RuleSet("Base", () =>
                            {
                                RuleFor(user => user.Name).NotNull().NotEmpty();
                                RuleFor(user => user.UserName).NotNull().NotEmpty();
                                RuleFor(user => user.Password).NotNull().NotEmpty();
                            });

            RuleSet("LoginValidate", () =>
            {
                Custom(LoginValidate);
            });
        }

        public ValidationFailure LoginValidate(User user, ValidationContext<User> context)
        {
            var currentUser = _userRepository.FindBy(user.Id);
            if (currentUser.IsNull() || currentUser.Status.Equals(GlobalConstants.StatusDeactivated))
                return new ValidationFailure("User", "El usuario esta desactivado o no existe");

            return null;
        }
    }
}