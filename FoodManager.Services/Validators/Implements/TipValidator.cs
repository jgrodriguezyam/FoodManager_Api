using FluentValidation;
using FoodManager.Infrastructure.Validators;
using FoodManager.Model;
using FoodManager.Services.Validators.Interfaces;

namespace FoodManager.Services.Validators.Implements
{
    public class TipValidator : BaseValidator<Tip>, ITipValidator
    {
        public TipValidator()
        {
            RuleSet("Base", () =>
            {
                RuleFor(tip => tip.Name).NotNull().NotEmpty();
            });
        }
    }
}