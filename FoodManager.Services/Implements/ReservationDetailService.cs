using System.Collections.Generic;
using FastMapper;
using FoodManager.DTO;
using FoodManager.DTO.Message.ReservationDetails;
using FoodManager.Infrastructure.Exceptions;
using FoodManager.Model.IRepositories;
using FoodManager.Queries.ReservationDetails;
using FoodManager.Services.Factories.Interfaces;
using FoodManager.Services.Interfaces;

namespace FoodManager.Services.Implements
{
    public class ReservationDetailService : IReservationDetailService
    {
        private readonly IReservationDetailQuery _reservationDetailQuery;
        private readonly IReservationDetailRepository _reservationDetailRepository;
        private readonly IReservationDetailFactory _reservationDetailFactory;

        public ReservationDetailService(IReservationDetailQuery reservationDetailQuery, IReservationDetailRepository reservationDetailRepository, IReservationDetailFactory reservationDetailFactory)
        {
            _reservationDetailQuery = reservationDetailQuery;
            _reservationDetailRepository = reservationDetailRepository;
            _reservationDetailFactory = reservationDetailFactory;
        }

        public FindReservationDetailsResponse Find(FindReservationDetailsRequest request)
        {
            try
            {
                _reservationDetailQuery.WithReservation(request.ReservationId);
                _reservationDetailQuery.Sort(request.Sort, request.SortBy);
                var totalRecords = _reservationDetailQuery.TotalRecords();
                _reservationDetailQuery.Paginate(request.StartPage, request.EndPage);
                var reservationDetails = _reservationDetailQuery.Execute();

                return new FindReservationDetailsResponse
                {
                    ReservationDetails = TypeAdapter.Adapt<List<ReservationDetailResponse>>(reservationDetails),
                    TotalRecords = totalRecords
                };
            }
            catch (DataAccessException)
            {
                throw new ApplicationException();
            }
        }

        public ReservationDetail Get(GetReservationDetailRequest request)
        {
            try
            {
                var reservationDetail = _reservationDetailRepository.FindBy(request.Id);
                reservationDetail.ThrowExceptionIfRecordIsNull();
                return _reservationDetailFactory.Execute(reservationDetail);
            }
            catch (DataAccessException)
            {
                throw new ApplicationException();
            }
        }
    }
}