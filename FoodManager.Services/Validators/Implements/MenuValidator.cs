using System;
using System.Linq;
using FluentValidation;
using FluentValidation.Results;
using FoodManager.Infrastructure.Constants;
using FoodManager.Infrastructure.Dates.Enums;
using FoodManager.Infrastructure.Enums;
using FoodManager.Infrastructure.Integers;
using FoodManager.Infrastructure.Objects;
using FoodManager.Infrastructure.Validators;
using FoodManager.Model;
using FoodManager.Model.Enums;
using FoodManager.Model.IRepositories;
using FoodManager.Services.Validators.Interfaces;

namespace FoodManager.Services.Validators.Implements
{
    public class MenuValidator : BaseValidator<Menu>, IMenuValidator
    {
        private readonly IDealerRepository _dealerRepository;
        private readonly ISaucerRepository _saucerRepository;
        private readonly IMenuRepository _menuRepository;
        private readonly DateTime _today = DateTime.Now;

        public MenuValidator(IDealerRepository dealerRepository, ISaucerRepository saucerRepository, IMenuRepository menuRepository)
        {
            _dealerRepository = dealerRepository;
            _saucerRepository = saucerRepository;
            _menuRepository = menuRepository;

            RuleSet("Base", () =>
            {
                RuleFor(menu => menu.Type).NotNull().NotEmpty();
                RuleFor(menu => menu.StartDate).NotNull().NotEmpty();
                RuleFor(menu => menu.EndDate).NotNull().NotEmpty();
                RuleFor(menu => menu.MaxAmount).NotNull().NotEmpty();
                RuleFor(menu => menu.DealerId).Must(dealerId => dealerId.IsNotZero()).WithMessage("Tienes que elegir un distribuidor");
                RuleFor(menu => menu.SaucerId).Must(saucerId => saucerId.IsNotZero()).WithMessage("Tienes que elegir un platillo");
                Custom(ReferencesValidate);
                Custom(DatesValidate);
                Custom(MaxAmountValidate);
            });

            RuleSet("Create", () =>
            {
                Custom(TodayValidate);
            });

            RuleSet("Update", () =>
            {
                Custom(EditStartDateValidate);
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

            var menuType = new MenuType().ConvertToCollection().FirstOrDefault(menuTp => menuTp.Value == menu.Type);
            if (menuType.IsNull())
                return new ValidationFailure("Menu", "El tipo de menu no existe");
            
            return null;
        }

        public ValidationFailure DatesValidate(Menu menu, ValidationContext<Menu> context)
        {
            if (menu.StartDate > menu.EndDate)
                return new ValidationFailure("Menu", "La fecha de inicio es mayor a fecha de fin");

            var dayWeek = new DayWeek().ConvertToCollection().FirstOrDefault(dayWee => dayWee.Value == menu.DayWeek);
            if (dayWeek.IsNull())
                return new ValidationFailure("Menu", "El dia de la semana no existe");

            return null;
        }

        public ValidationFailure MaxAmountValidate(Menu menu, ValidationContext<Menu> context)
        {
            if (menu.MaxAmount < menu.Limit)
                return new ValidationFailure("Menu", "El limite es mayor a cantidad maxima");

            return null;
        }

        public ValidationFailure TodayValidate(Menu menu, ValidationContext<Menu> context)
        {
            if (menu.StartDate.Date < _today.Date)
                return new ValidationFailure("Menu", "La fecha de inicio es menor a fecha de hoy");

            return null;
        }

        public ValidationFailure EditStartDateValidate(Menu menu, ValidationContext<Menu> context)
        {
            var currentMenu = _menuRepository.FindBy(menu.Id);
            if (menu.StartDate.Date < _today.Date && currentMenu.StartDate.Date != menu.StartDate.Date)
                return new ValidationFailure("Menu", "La fecha de inicio es menor a fecha de hoy");

            return null;
        }
    }
}