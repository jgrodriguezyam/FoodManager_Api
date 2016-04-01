using System.Collections.Generic;
using System.Linq;
using FastMapper;
using FoodManager.DTO.BaseRequest;
using FoodManager.DTO.BaseResponse;
using FoodManager.DTO.Message.Dealers;
using FoodManager.Infrastructure.Exceptions;
using FoodManager.Model;
using FoodManager.Model.IRepositories;
using FoodManager.Queries.Dealers;
using FoodManager.Services.Interfaces;
using FoodManager.Services.Validators.Interfaces;

namespace FoodManager.Services.Implements
{
    public class DealerService : IDealerService
    {
        private readonly IDealerQuery _dealerQuery;
        private readonly IDealerRepository _dealerRepository;
        private readonly IDealerValidator _dealerValidator;
        private readonly IDealerSaucerRepository _dealerSaucerRepository;
        private readonly IDealerSaucerValidator _dealerSaucerValidator;

        public DealerService(IDealerQuery dealerQuery, IDealerRepository dealerRepository, IDealerValidator dealerValidator, IDealerSaucerRepository dealerSaucerRepository, IDealerSaucerValidator dealerSaucerValidator)
        {
            _dealerQuery = dealerQuery;
            _dealerRepository = dealerRepository;
            _dealerValidator = dealerValidator;
            _dealerSaucerRepository = dealerSaucerRepository;
            _dealerSaucerValidator = dealerSaucerValidator;
        }

        public FindDealersResponse Find(FindDealersRequest request)
        {
            try
            {
                _dealerQuery.WithOnlyActivated(true);
                _dealerQuery.WithOnlyStatusActivated(request.OnlyStatusActivated);
                _dealerQuery.WithOnlyStatusDeactivated(request.OnlyStatusDeactivated);
                _dealerQuery.WithName(request.Name);
                _dealerQuery.Sort(request.Sort, request.SortBy);
                var totalRecords = _dealerQuery.TotalRecords();
                _dealerQuery.Paginate(request.StartPage, request.EndPage);
                var dealers = _dealerQuery.Execute();

                return new FindDealersResponse
                {
                    Dealers = TypeAdapter.Adapt<List<DealerResponse>>(dealers),
                    TotalRecords = totalRecords
                };
            }
            catch (DataAccessException)
            {
                throw new ApplicationException();
            }
        }

        public CreateResponse Create(DealerRequest request)
        {
            try
            {
                var dealer = TypeAdapter.Adapt<Dealer>(request);
                _dealerValidator.ValidateAndThrowException(dealer, "Base");
                _dealerRepository.Add(dealer);
                return new CreateResponse(dealer.Id);
            }
            catch (DataAccessException)
            {
                throw new ApplicationException();
            }
        }

        public SuccessResponse Update(DealerRequest request)
        {
            try
            {
                var currentDealer = _dealerRepository.FindBy(request.Id);
                currentDealer.ThrowExceptionIfRecordIsNull();
                var dealerToCopy = TypeAdapter.Adapt<Dealer>(request);
                TypeAdapter.Adapt(dealerToCopy, currentDealer);
                _dealerValidator.ValidateAndThrowException(currentDealer, "Base");
                _dealerRepository.Update(currentDealer);
                return new SuccessResponse { IsSuccess = true };
            }
            catch (DataAccessException)
            {
                throw new ApplicationException();
            }
        }

        public DTO.Dealer Get(GetDealerRequest request)
        {
            try
            {
                var dealer = _dealerRepository.FindBy(request.Id);
                dealer.ThrowExceptionIfRecordIsNull();
                return TypeAdapter.Adapt<DTO.Dealer>(dealer);
            }
            catch (DataAccessException)
            {
                throw new ApplicationException();
            }
        }

        public SuccessResponse Delete(DeleteDealerRequest request)
        {
            try
            {
                var dealer = _dealerRepository.FindBy(request.Id);
                dealer.ThrowExceptionIfRecordIsNull();
                var isReference = _dealerRepository.IsReference(request.Id);
                isReference.ThrowExceptionIfIsReference();
                _dealerRepository.Remove(dealer);
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
                var dealer = _dealerRepository.FindBy(request.Id);
                dealer.ThrowExceptionIfRecordIsNull();
                dealer.Status.ThrowExceptionIfIsSameStatus(request.Status);
                dealer.Status = request.Status;
                _dealerRepository.Update(dealer);
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
                var isReference = _dealerRepository.IsReference(request.Id);
                return new SuccessResponse { IsSuccess = isReference };
            }
            catch (DataAccessException)
            {
                throw new ApplicationException();
            }
        }

        public SuccessResponse AddSaucer(RelationRequest request)
        {
            try
            {
                var dealerSaucer = TypeAdapter.Adapt<DealerSaucer>(request);
                _dealerSaucerValidator.ValidateAndThrowException(dealerSaucer, "Base");
                _dealerSaucerRepository.Add(dealerSaucer);
                return new SuccessResponse { IsSuccess = true };
            }
            catch (DataAccessException)
            {
                throw new ApplicationException();
            }
        }

        public SuccessResponse RemoveSaucer(RelationRequest request)
        {
            try
            {
                var dealerSaucer = _dealerSaucerRepository.FindBy(dealerSaucerRe => dealerSaucerRe.DealerId == request.FirstReference && dealerSaucerRe.SaucerId == request.SecondReference).FirstOrDefault();
                dealerSaucer.ThrowExceptionIfRecordIsNull();
                _dealerSaucerRepository.Remove(dealerSaucer);
                return new SuccessResponse { IsSuccess = true };
            }
            catch (DataAccessException)
            {
                throw new ApplicationException();
            }
        }
    }
}