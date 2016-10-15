﻿using System;
using System.Linq;
using System.Net;
using FluentValidation;
using FluentValidation.Results;
using FoodManager.Infrastructure.Collections;
using FoodManager.Infrastructure.Constants;
using FoodManager.Infrastructure.Dates.Enums;
using FoodManager.Infrastructure.Enums;
using FoodManager.Infrastructure.Exceptions;
using FoodManager.Infrastructure.Integers;
using FoodManager.Infrastructure.Objects;
using FoodManager.Infrastructure.Validators;
using FoodManager.Infrastructure.Validators.Enums;
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
                RuleFor(menu => menu.MealType).NotNull().NotEmpty();
                RuleFor(menu => menu.StartDate).NotNull().NotEmpty();
                RuleFor(menu => menu.EndDate).NotNull().NotEmpty();
                RuleFor(menu => menu.MaxAmount).NotNull().NotEmpty();
                RuleFor(menu => menu.DealerId).Must(dealerId => dealerId.IsNotZero()).WithMessage("Tienes que elegir un distribuidor");
                RuleFor(menu => menu.SaucerId).Must(saucerId => saucerId.IsNotZero()).WithMessage("Tienes que elegir un platillo");
                Custom(ReferencesValidate);
                Custom(DatesValidate);
            });

            RuleSet("Create", () =>
            {
                Custom(CreateValidate);
            });

            RuleSet("Update", () =>
            {
                Custom(EditValidate);
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

            var mealType = new MealType().ConvertToCollection().FirstOrDefault(mealTp => mealTp.Value == menu.MealType);
            if (mealType.IsNull())
                return new ValidationFailure("Menu", "El tipo de comida no existe");
            
            return null;
        }

        public ValidationFailure DatesValidate(Menu menu, ValidationContext<Menu> context)
        {
            if (menu.StartDate > menu.EndDate)
                return new ValidationFailure("Menu", "La fecha de inicio es mayor a fecha de fin");

            var dayWeek = new DayWeek().ConvertToCollection().FirstOrDefault(dayWee => dayWee.Value == menu.DayWeek);
            if (dayWeek.IsNull())
                return new ValidationFailure("Menu", "El dia de la semana no existe");

            if (menu.StartDate.IsNull() || menu.EndDate.IsNull())
                ExceptionExtensions.ThrowCustomException(HttpStatusCode.Conflict, CodeValidator.InvalidDate.GetValue(), "Fecha null");

            var menus = _menuRepository.FindBy(currentMenu => currentMenu.DealerId == menu.DealerId && currentMenu.SaucerId == menu.SaucerId && currentMenu.Id != menu.Id && currentMenu.MealType == menu.MealType && currentMenu.IsActive);
            var menusInvalidStartDate = menus.Where(currentMenu => currentMenu.StartDate <= menu.StartDate && currentMenu.EndDate >= menu.StartDate);
            var menusInvalidEndDate = menus.Where(currentMenu => currentMenu.StartDate <= menu.EndDate && currentMenu.EndDate >= menu.EndDate);

            if (menusInvalidStartDate.IsNotEmpty())
                ExceptionExtensions.ThrowCustomException(HttpStatusCode.Conflict, CodeValidator.InvalidDate.GetValue(), "La fecha de inicio del platillo ya está siendo utilizado en otro menu");

            if (menusInvalidEndDate.IsNotEmpty())
                ExceptionExtensions.ThrowCustomException(HttpStatusCode.Conflict, CodeValidator.InvalidDate.GetValue(), "La fecha final del platillo ya está siendo utilizado en otro menu");

            var menusInvalidCurrentStartDate = menus.Where(currentMenu => currentMenu.StartDate >= menu.StartDate && currentMenu.StartDate <= menu.EndDate);
            var menusInvalidCurrentEndtDate = menus.Where(currentMenu => currentMenu.EndDate >= menu.StartDate && currentMenu.EndDate <= menu.EndDate);

            if (menusInvalidCurrentStartDate.IsNotEmpty())
                ExceptionExtensions.ThrowCustomException(HttpStatusCode.Conflict, CodeValidator.InvalidDate.GetValue(), "La configuración del platillo ya está siendo utilizado en otro menu");

            if (menusInvalidCurrentEndtDate.IsNotEmpty())
                ExceptionExtensions.ThrowCustomException(HttpStatusCode.Conflict, CodeValidator.InvalidDate.GetValue(), "La configuración del platillo ya está siendo utilizado en otro menu");
            
            return null;
        }

        public ValidationFailure CreateValidate(Menu menu, ValidationContext<Menu> context)
        {
            if (menu.StartDate.Date < _today.Date)
                return new ValidationFailure("Menu", "La fecha de inicio es menor a fecha de hoy");

            return null;
        }

        public ValidationFailure EditValidate(Menu menu, ValidationContext<Menu> context)
        {
            var currentMenu = _menuRepository.FindBy(menu.Id);
            if (menu.StartDate.Date < _today.Date && currentMenu.StartDate.Date != menu.StartDate.Date)
                return new ValidationFailure("Menu", "La fecha de inicio es menor a fecha de hoy");

            return null;
        }
    }
}