using FluentValidation;
using FluentValidation.Results;
using FoodManager.Infrastructure.Constants;
using FoodManager.Infrastructure.Integers;
using FoodManager.Infrastructure.Objects;
using FoodManager.Infrastructure.Validators;
using FoodManager.Model;
using FoodManager.Model.IRepositories;
using FoodManager.Services.Validators.Interfaces;

namespace FoodManager.Services.Validators.Implements
{
    public class MenuValidator : BaseValidator<Menu>, IMenuValidator
    {
        private readonly IDealerRepository _dealerRepository;
        private readonly ISaucerRepository _saucerRepository;

        public MenuValidator(IDealerRepository dealerRepository, ISaucerRepository saucerRepository)
        {
            _dealerRepository = dealerRepository;
            _saucerRepository = saucerRepository;

            RuleSet("Base", () =>
            {
                RuleFor(menu => menu.Name).NotNull().NotEmpty();
                RuleFor(menu => menu.DayWeek).NotNull().NotEmpty();
                RuleFor(menu => menu.Type).NotNull().NotEmpty();
                RuleFor(menu => menu.Limit).NotNull().NotEmpty();
                RuleFor(menu => menu.StartDate).NotNull().NotEmpty();
                RuleFor(menu => menu.EndDate).NotNull().NotEmpty();
                RuleFor(menu => menu.MaxAmount).NotNull().NotEmpty();
                RuleFor(menu => menu.DealerId).Must(dealerId => dealerId.IsNotZero()).WithMessage("Tienes que elegir un distribuidor");
                RuleFor(menu => menu.SaucerId).Must(saucerId => saucerId.IsNotZero()).WithMessage("Tienes que elegir un platillo");
                Custom(ReferencesValidate);
            });
        }

        public ValidationFailure ReferencesValidate(Menu menu, ValidationContext<Menu> context)
        {
            var dealer = _dealerRepository.FindBy(menu.DealerId);
            if (dealer.IsNull() || dealer.Status.Equals(GlobalConstants.StatusDeactivated))
                return new ValidationFailure("Menu", "El distribuidor esta desactivado o no existe");

            var saucer = _saucerRepository.FindBy(menu.SaucerId);
            if (saucer.IsNull() || saucer.Status.Equals(GlobalConstants.StatusDeactivated))
                return new ValidationFailure("Menu", "El platillo esta desactivado o no existe");

            return null;
        }
    }
}