using System;
using FluentValidation;
using FluentValidation.Results;
using FoodManager.Infrastructure.Collections;
using FoodManager.Infrastructure.Constants;
using FoodManager.Infrastructure.Decimals;
using FoodManager.Infrastructure.Integers;
using FoodManager.Infrastructure.Objects;
using FoodManager.Infrastructure.Validators;
using FoodManager.Model;
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
                RuleFor(reservation => reservation.DealerId).Must(dealerId => dealerId.IsNotZero()).WithMessage("Tienes que elegir un distribuidor");
                RuleFor(reservation => reservation.Portion).Must(portion => portion.IsNotZero()).WithMessage("Tienes que elegir una porcion");
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

            var dealer = _dealerRepository.FindBy(reservation.DealerId);
            if (dealer.IsNull() || dealer.Status.Equals(GlobalConstants.StatusDeactivated))
                return new ValidationFailure("Reservation", "El distribuidor esta desactivado o no existe");

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
                                                                                   reservatio.DealerId == reservation.DealerId);
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
                                                                                   reservatio.Id != reservation.Id);
            if (reservationRetrieved.IsNotEmpty())
                return new ValidationFailure("Reservation", "Ya existe una reservacion para ese platillo");

            return null;
        }
    }
}