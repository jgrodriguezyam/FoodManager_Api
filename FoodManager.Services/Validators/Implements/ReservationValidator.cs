using System;
using System.Linq;
using FluentValidation;
using FluentValidation.Results;
using FoodManager.Infrastructure.Collections;
using FoodManager.Infrastructure.Constants;
using FoodManager.Infrastructure.Decimals;
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
    public class ReservationValidator : BaseValidator<Reservation>, IReservationValidator
    {
        private readonly IWorkerRepository _workerRepository;
        private readonly ISaucerRepository _saucerRepository;
        private readonly IDealerRepository _dealerRepository;
        private readonly IReservationRepository _reservationRepository;
        private readonly DateTime _today = DateTime.Now;

        public ReservationValidator(IWorkerRepository workerRepository, ISaucerRepository saucerRepository, IDealerRepository dealerRepository, IReservationRepository reservationRepository)
        {
            _workerRepository = workerRepository;
            _saucerRepository = saucerRepository;
            _dealerRepository = dealerRepository;
            _reservationRepository = reservationRepository;

            RuleSet("Base", () =>
            {
                RuleFor(reservation => reservation.Date).NotNull().NotEmpty();
                RuleFor(reservation => reservation.WorkerId).Must(workerId => workerId.IsNotZero()).WithMessage("Tienes que elegir un trabajador");
                RuleFor(reservation => reservation.SaucerId).Must(saucerId => saucerId.IsNotZero()).WithMessage("Tienes que elegir un platillo");
                RuleFor(reservation => reservation.Portion).Must(portion => portion.IsNotZero()).WithMessage("Tienes que elegir una porcion");
                //RuleFor(reservation => reservation.MealType).NotNull().NotEmpty();
                Custom(ReferencesValidate);
                Custom(DateValidate);
            });

            RuleSet("Create", () =>
            {
                Custom(CreateReservationValidate);
            });

            RuleSet("Update", () =>
            {
                Custom(UpdateReservationValidate);
            });
        }

        public ValidationFailure ReferencesValidate(Reservation reservation, ValidationContext<Reservation> context)
        {
            var worker = _workerRepository.FindBy(reservation.WorkerId);
            if (worker.IsNull() || worker.Status.Equals(GlobalConstants.StatusDeactivated))
                return new ValidationFailure("Reservation", "El trabajador esta desactivado o no existe");

            var saucer = _saucerRepository.FindBy(reservation.SaucerId);
            if (saucer.IsNull() || saucer.Status.Equals(GlobalConstants.StatusDeactivated))
                return new ValidationFailure("Reservation", "El platillo esta desactivado o no existe");

            var dealerId = reservation.DealerId ?? 0;
            if (dealerId.IsNotZero())
            {
                var dealer = _dealerRepository.FindBy(dealerId);
                if (dealer.IsNull() || dealer.Status.Equals(GlobalConstants.StatusDeactivated))
                    return new ValidationFailure("Reservation", "El distribuidor esta desactivado o no existe");
            }

            var mealType = new MealType().ConvertToCollection().FirstOrDefault(mealTp => mealTp.Value == reservation.MealType);
            if (mealType.IsNull())
                return new ValidationFailure("Reservation", "El tipo de comida no existe");

            return null;
        }

        public ValidationFailure DateValidate(Reservation reservation, ValidationContext<Reservation> context)
        {
            if (reservation.Date.Date < _today.Date)
                return new ValidationFailure("Reservation", "La fecha de reservacion es menor a la fecha de hoy");

            return null;
        }

        public ValidationFailure CreateReservationValidate(Reservation reservation, ValidationContext<Reservation> context)
        {
            var reservationRetrieved = _reservationRepository.FindBy(reservatio => reservatio.Date == reservation.Date &&
                                                                                   reservatio.SaucerId == reservation.SaucerId &&
                                                                                   reservatio.WorkerId == reservation.WorkerId &&
                                                                                   reservatio.DealerId == reservation.DealerId && 
                                                                                   reservatio.MealType == reservation.MealType &&
                                                                                   reservatio.IsActive);
            if (reservationRetrieved.IsNotEmpty())
                return new ValidationFailure("Reservation", "Ya existe una reservacion para ese platillo");

            return null;
        }

        public ValidationFailure UpdateReservationValidate(Reservation reservation, ValidationContext<Reservation> context)
        {
            var reservationRetrieved = _reservationRepository.FindBy(reservatio => reservatio.Date == reservation.Date &&
                                                                                   reservatio.SaucerId == reservation.SaucerId &&
                                                                                   reservatio.WorkerId == reservation.WorkerId &&
                                                                                   reservatio.DealerId == reservation.DealerId &&
                                                                                   reservatio.MealType == reservation.MealType &&
                                                                                   reservatio.Id != reservation.Id &&
                                                                                   reservatio.IsActive);
            if (reservationRetrieved.IsNotEmpty())
                return new ValidationFailure("Reservation", "Ya existe una reservacion para ese platillo");

            return null;
        }
    }
}