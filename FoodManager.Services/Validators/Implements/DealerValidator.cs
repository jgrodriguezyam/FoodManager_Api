using FluentValidation;
using FoodManager.Infrastructure.Validators;
using FoodManager.Model;
using FoodManager.Services.Validators.Interfaces;

namespace FoodManager.Services.Validators.Implements
{
    public class DealerValidator : BaseValidator<Dealer>, IDealerValidator
    {
        public DealerValidator()
        {
            RuleSet("Base", () =>
            {
                RuleFor(dealer => dealer.Name).NotNull().NotEmpty();
            });
        }
    }
}