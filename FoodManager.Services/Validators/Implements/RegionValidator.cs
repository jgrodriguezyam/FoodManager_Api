using FluentValidation;
using FoodManager.Infrastructure.Validators;
using FoodManager.Model;
using FoodManager.Services.Validators.Interfaces;

namespace FoodManager.Services.Validators.Implements
{
    public class RegionValidator : BaseValidator<Region>, IRegionValidator
    {
        public RegionValidator()
        {
            RuleSet("Base", () =>
                            {
                                RuleFor(user => user.Name).NotNull().NotEmpty();
                            });
        }
    }
}