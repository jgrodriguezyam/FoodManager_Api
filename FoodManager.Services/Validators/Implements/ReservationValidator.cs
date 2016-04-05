using System;
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
    public class ReservationValidator : BaseValidator<Reservation>, IReservationValidator
    {
        private readonly IWorkerRepository _workerRepository;
        private readonly ISaucerRepository _saucerRepository;
        private readonly DateTime _today = DateTime.Now;

        public ReservationValidator(IWorkerRepository workerRepository, ISaucerRepository saucerRepository)
        {
            _workerRepository = workerRepository;
            _saucerRepository = saucerRepository;

            RuleSet("Base", () =>
            {
                RuleFor(reservation => reservation.Date).NotNull().NotEmpty();
                RuleFor(reservation => reservation.Energy).NotNull().NotEmpty();
                RuleFor(reservation => reservation.Protein).NotNull().NotEmpty();
                RuleFor(reservation => reservation.Carbohydrate).NotNull().NotEmpty();
                RuleFor(reservation => reservation.Sugar).NotNull().NotEmpty();
                RuleFor(reservation => reservation.Lipid).NotNull().NotEmpty();
                RuleFor(reservation => reservation.Sodium).NotNull().NotEmpty();
                RuleFor(reservation => reservation.WorkerId).Must(workerId => workerId.IsNotZero()).WithMessage("Tienes que elegir un trabajador");
                RuleFor(reservation => reservation.SaucerId).Must(saucerId => saucerId.IsNotZero()).WithMessage("Tienes que elegir un platillo");
                Custom(ReferencesValidate);
                Custom(DateValidate);
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

            return null;
        }

        public ValidationFailure DateValidate(Reservation reservation, ValidationContext<Reservation> context)
        {
            if (reservation.Date.Date != _today.Date)
                return new ValidationFailure("Menu", "La fecha de inicio es menor o mayor a la fecha de hoy");

            return null;
        }
    }
}