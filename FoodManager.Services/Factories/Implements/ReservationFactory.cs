using System.Collections.Generic;
using System.Linq;
using FastMapper;
using FoodManager.DTO.Message.Dealers;
using FoodManager.DTO.Message.Reservations;
using FoodManager.DTO.Message.Saucers;
using FoodManager.DTO.Message.Workers;
using FoodManager.Infrastructure.Dates;
using FoodManager.Infrastructure.Decimals;
using FoodManager.Infrastructure.Enums;
using FoodManager.Infrastructure.Integers;
using FoodManager.Model;
using FoodManager.Model.Enums;
using FoodManager.Model.IRepositories;
using FoodManager.Services.Factories.Interfaces;
using ServiceStack.Common.Extensions;

namespace FoodManager.Services.Factories.Implements
{
    public class ReservationFactory : IReservationFactory
    {
        private readonly IWorkerRepository _workerRepository;
        private readonly ISaucerRepository _saucerRepository;
        private readonly IDealerRepository _dealerRepository;
        private readonly IReservationRepository _reservationRepository;
        private readonly IIngredientRepository _ingredientRepository;
        private readonly IReservationDetailRepository _reservationDetailRepository;
        
        public ReservationFactory(IWorkerRepository workerRepository, ISaucerRepository saucerRepository, IDealerRepository dealerRepository, IReservationRepository reservationRepository, IIngredientRepository ingredientRepository, IReservationDetailRepository reservationDetailRepository)
        {
            _workerRepository = workerRepository;
            _saucerRepository = saucerRepository;
            _dealerRepository = dealerRepository;
            _reservationRepository = reservationRepository;
            _ingredientRepository = ingredientRepository;
            _reservationDetailRepository = reservationDetailRepository;
        }

        public ReservationResponse Execute(Reservation reservation)
        {
            return AppendProperties(new[] { reservation }).FirstOrDefault();
        }

        public IEnumerable<ReservationResponse> Execute(IEnumerable<Reservation> reservations)
        {
            return AppendProperties(reservations);
        }

        private IEnumerable<ReservationResponse> AppendProperties(IEnumerable<Reservation> reservations)
        {
            var reservationsResponse = TypeAdapter.Adapt<List<ReservationResponse>>(reservations);
            var workers = _workerRepository.FindBy(worker => worker.IsActive);
            var saucers = _saucerRepository.FindBy(saucer => saucer.IsActive);
            var dealers = _dealerRepository.FindBy(dealer => dealer.IsActive);

            reservationsResponse.ForEach(reservationResponse =>
            {
                var reservation = reservations.First(reservationModel => reservationModel.Id == reservationResponse.Id);
                var worker = workers.First(workerModel => workerModel.Id == reservation.WorkerId);
                reservationResponse.Worker = TypeAdapter.Adapt<WorkerResponse>(worker);
                var saucer = saucers.First(saucerModel => saucerModel.Id == reservation.SaucerId);
                reservationResponse.Saucer = TypeAdapter.Adapt<SaucerResponse>(saucer);
                var dealerId = reservation.DealerId ?? 0;
                if (dealerId.IsNotZero())
                {
                    var dealer = dealers.First(dealerModel => dealerModel.Id == dealerId);
                    reservationResponse.Dealer = TypeAdapter.Adapt<DealerResponse>(dealer);
                }
            });

            return reservationsResponse;
        }

        public ReservationReportResponse Execute(ReservationReportRequest reservationReportRequest)
        {
            var reservations = _reservationRepository.FindBy(reservation =>
                                                             reservation.WorkerId == reservationReportRequest.WorkerId &&
                                                             reservation.Date >= reservationReportRequest.StartDate.DateStringToDateTime() &&
                                                             reservation.Date <= reservationReportRequest.EndDate.DateStringToDateTime() &&
                                                             reservation.Status).ToList();
            var dates = reservations.GroupBy(reservation => reservation.Date);
            var reservationDetails = _reservationDetailRepository.FindBy(saucerConfiguration => saucerConfiguration.Status);
            var ingredients = _ingredientRepository.FindBy(ingredient => ingredient.Status);
            var reservationReport = new ReservationReportResponse();

            dates.ForEach(date =>
            {
                var reservationsByDate = reservations.Where(reservation => reservation.Date == date.Key).ToList();
                var breakfastReservations = reservationsByDate.Where(reservation => reservation.MealType == MealType.Breakfast.GetValue());
                var lunchReservations = reservationsByDate.Where(reservation => reservation.MealType == MealType.Lunch.GetValue());
                var dinnerReservations = reservationsByDate.Where(reservation => reservation.MealType == MealType.Dinner.GetValue());

                var reservationCaloryReport = new ReservationCaloryReportResponse();
                reservationCaloryReport.Date = date.Key.ToDateString();
                breakfastReservations.ForEach(reservation =>
                {
                    reservationCaloryReport.Breakfast += GetTotalCaloriesByReservation(reservation.Id, reservationDetails,
                        ingredients);
                });
                lunchReservations.ForEach(reservation =>
                {
                    reservationCaloryReport.Lunch += GetTotalCaloriesByReservation(reservation.Id, reservationDetails,
                        ingredients);
                });
                dinnerReservations.ForEach(reservation =>
                {
                    reservationCaloryReport.Dinner += GetTotalCaloriesByReservation(reservation.Id, reservationDetails,
                        ingredients);
                });

                if (reservationCaloryReport.Breakfast.IsNotZero() && reservationCaloryReport.Lunch.IsNotZero() && reservationCaloryReport.Dinner.IsNotZero())
                    reservationReport.Calories.Add(reservationCaloryReport);
            });

            return reservationReport;
        }

        private decimal GetTotalCaloriesByReservation(int reservationId, IEnumerable<ReservationDetail> allReservationDetails, IEnumerable<Ingredient> allIngredients)
        {
            decimal totalCalories = 0;
            var reservationDetails = allReservationDetails.Where(reservationDetail => reservationDetail.ReservationId == reservationId);
            reservationDetails.ForEach(reservationDetail =>
            {
                var ingredient = allIngredients.First(ingredientModel => ingredientModel.Id == reservationDetail.IngredientId);
                var netWeightConfiguration = reservationDetail.NetWeight;
                totalCalories += (ingredient.Energy / ingredient.NetWeight) * netWeightConfiguration;
            });

            return totalCalories;
        }
    }
}