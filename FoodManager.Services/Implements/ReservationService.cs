using System.Linq;
using FastMapper;
using FoodManager.DTO.BaseRequest;
using FoodManager.DTO.BaseResponse;
using FoodManager.DTO.Message.Reservations;
using FoodManager.Infrastructure.Exceptions;
using FoodManager.Model;
using FoodManager.Model.IRepositories;
using FoodManager.Queries.Reservations;
using FoodManager.Services.Factories.Interfaces;
using FoodManager.Services.Interfaces;
using FoodManager.Services.Validators.Interfaces;

namespace FoodManager.Services.Implements
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationQuery _reservationQuery;
        private readonly IReservationRepository _reservationRepository;
        private readonly IReservationValidator _reservationValidator;
        private readonly IReservationFactory _reservationFactory;
        private readonly IReservationDetailFactory _reservationDetailFactory;
        private readonly IReservationDetailRepository _reservationDetailRepository;

        public ReservationService(IReservationQuery reservationQuery, IReservationRepository reservationRepository, IReservationValidator reservationValidator, IReservationFactory reservationFactory, IReservationDetailFactory reservationDetailFactory, IReservationDetailRepository reservationDetailRepository)
        {
            _reservationQuery = reservationQuery;
            _reservationRepository = reservationRepository;
            _reservationValidator = reservationValidator;
            _reservationFactory = reservationFactory;
            _reservationDetailFactory = reservationDetailFactory;
            _reservationDetailRepository = reservationDetailRepository;
        }

        public FindReservationsResponse Find(FindReservationsRequest request)
        {
            try
            {
                _reservationQuery.WithOnlyActivated(true);
                _reservationQuery.WithOnlyStatusActivated(request.OnlyStatusActivated);
                _reservationQuery.WithOnlyStatusDeactivated(request.OnlyStatusDeactivated);
                _reservationQuery.WithWorker(request.WorkerId);
                _reservationQuery.WithSaucer(request.SaucerId);
                _reservationQuery.WithDealer(request.DealerId);
                _reservationQuery.WithOnlyToday(request.OnlyToday);
                _reservationQuery.WithDate(request.Date);
                _reservationQuery.WithPortion(request.Portion);
                _reservationQuery.Sort(request.Sort, request.SortBy);
                var totalRecords = _reservationQuery.TotalRecords();
                _reservationQuery.Paginate(request.StartPage, request.EndPage);
                var reservations = _reservationQuery.Execute();

                return new FindReservationsResponse
                {
                    Reservations = _reservationFactory.Execute(reservations).ToList(),
                    TotalRecords = totalRecords
                };
            }
            catch (DataAccessException)
            {
                throw new ApplicationException();
            }
        }

        public CreateResponse Create(ReservationRequest request)
        {
            try
            {
                var reservation = TypeAdapter.Adapt<Reservation>(request);
                _reservationValidator.ValidateAndThrowException(reservation, "Base,Create");
                _reservationRepository.Add(reservation);
                var reservationDetails = _reservationDetailFactory.BySaucer(reservation.SaucerId);
                reservationDetails.ForEach(reservationDetail =>
                                           {
                                               reservationDetail.ReservationId = reservation.Id;
                                               _reservationDetailRepository.Add(reservationDetail);
                                           });
                return new CreateResponse(reservation.Id);
            }
            catch (DataAccessException)
            {
                throw new ApplicationException();
            }
        }

        public SuccessResponse Update(ReservationRequest request)
        {
            try
            {
                var currentReservation = _reservationRepository.FindBy(request.Id);
                currentReservation.ThrowExceptionIfRecordIsNull();
                var reservationToCopy = TypeAdapter.Adapt<Reservation>(request);
                TypeAdapter.Adapt(reservationToCopy, currentReservation);
                _reservationValidator.ValidateAndThrowException(currentReservation, "Base,Update");
                _reservationRepository.Update(currentReservation);
                return new SuccessResponse { IsSuccess = true };
            }
            catch (DataAccessException)
            {
                throw new ApplicationException();
            }
        }

        public ReservationResponse Get(GetReservationRequest request)
        {
            try
            {
                var reservation = _reservationRepository.FindBy(request.Id);
                reservation.ThrowExceptionIfRecordIsNull();
                return _reservationFactory.Execute(reservation);
            }
            catch (DataAccessException)
            {
                throw new ApplicationException();
            }
        }

        public SuccessResponse Delete(DeleteReservationRequest request)
        {
            try
            {
                var reservation = _reservationRepository.FindBy(request.Id);
                reservation.ThrowExceptionIfRecordIsNull();
                var isReference = _reservationRepository.IsReference(request.Id);
                isReference.ThrowExceptionIfIsReference();
                _reservationRepository.Remove(reservation);
                return new SuccessResponse { IsSuccess = true };
            }
            catch (DataAccessException)
            {
                throw new ApplicationException();
            }
        }

        public SuccessResponse ChangeStatus(ChangeStatusRequest request)
        {
            try
            {
                var reservation = _reservationRepository.FindBy(request.Id);
                reservation.ThrowExceptionIfRecordIsNull();
                reservation.Status.ThrowExceptionIfIsSameStatus(request.Status);
                reservation.Status = request.Status;
                _reservationRepository.Update(reservation);
                return new SuccessResponse { IsSuccess = true };
            }
            catch (DataAccessException)
            {
                throw new ApplicationException();
            }
        }

        public SuccessResponse IsReference(IsReferenceRequest request)
        {
            try
            {
                var isReference = _reservationRepository.IsReference(request.Id);
                return new SuccessResponse { IsSuccess = isReference };
            }
            catch (DataAccessException)
            {
                throw new ApplicationException();
            }
        }
    }
}