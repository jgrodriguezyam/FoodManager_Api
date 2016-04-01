using System.Collections.Generic;
using FastMapper;
using FoodManager.DTO.BaseRequest;
using FoodManager.DTO.BaseResponse;
using FoodManager.DTO.Message.Tips;
using FoodManager.Infrastructure.Exceptions;
using FoodManager.Model;
using FoodManager.Model.IRepositories;
using FoodManager.Queries.Tips;
using FoodManager.Services.Interfaces;
using FoodManager.Services.Validators.Interfaces;

namespace FoodManager.Services.Implements
{
    public class TipService : ITipService
    {
        private readonly ITipQuery _tipQuery;
        private readonly ITipRepository _tipRepository;
        private readonly ITipValidator _tipValidator;

        public TipService(ITipQuery tipQuery, ITipRepository tipRepository, ITipValidator tipValidator)
        {
            _tipQuery = tipQuery;
            _tipRepository = tipRepository;
            _tipValidator = tipValidator;
        }

        public FindTipsResponse Find(FindTipsRequest request)
        {
            try
            {
                _tipQuery.WithOnlyActivated(true);
                _tipQuery.WithOnlyStatusActivated(request.OnlyStatusActivated);
                _tipQuery.WithOnlyStatusDeactivated(request.OnlyStatusDeactivated);
                _tipQuery.WithName(request.Name);
                _tipQuery.Sort(request.Sort, request.SortBy);
                var totalRecords = _tipQuery.TotalRecords();
                _tipQuery.Paginate(request.StartPage, request.EndPage);
                var tips = _tipQuery.Execute();

                return new FindTipsResponse
                {
                    Tips = TypeAdapter.Adapt<List<TipResponse>>(tips),
                    TotalRecords = totalRecords
                };
            }
            catch (DataAccessException)
            {
                throw new ApplicationException();
            }
        }

        public CreateResponse Create(TipRequest request)
        {
            try
            {
                var tip = TypeAdapter.Adapt<Tip>(request);
                _tipValidator.ValidateAndThrowException(tip, "Base");
                _tipRepository.Add(tip);
                return new CreateResponse(tip.Id);
            }
            catch (DataAccessException)
            {
                throw new ApplicationException();
            }
        }

        public SuccessResponse Update(TipRequest request)
        {
            try
            {
                var currentTip = _tipRepository.FindBy(request.Id);
                currentTip.ThrowExceptionIfRecordIsNull();
                var tipToCopy = TypeAdapter.Adapt<Tip>(request);
                TypeAdapter.Adapt(tipToCopy, currentTip);
                _tipValidator.ValidateAndThrowException(currentTip, "Base");
                _tipRepository.Update(currentTip);
                return new SuccessResponse { IsSuccess = true };
            }
            catch (DataAccessException)
            {
                throw new ApplicationException();
            }
        }

        public DTO.Tip Get(GetTipRequest request)
        {
            try
            {
                var tip = _tipRepository.FindBy(request.Id);
                tip.ThrowExceptionIfRecordIsNull();
                return TypeAdapter.Adapt<DTO.Tip>(tip);
            }
            catch (DataAccessException)
            {
                throw new ApplicationException();
            }
        }

        public SuccessResponse Delete(DeleteTipRequest request)
        {
            try
            {
                var tip = _tipRepository.FindBy(request.Id);
                tip.ThrowExceptionIfRecordIsNull();
                _tipRepository.Remove(tip);
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
                var tip = _tipRepository.FindBy(request.Id);
                tip.ThrowExceptionIfRecordIsNull();
                if (tip.Status.Equals(request.Status))
                    ExceptionExtensions.ThrowStatusException(request.Status);
                tip.Status = request.Status;
                _tipRepository.Update(tip);
                return new SuccessResponse { IsSuccess = true };
            }
            catch (DataAccessException)
            {
                throw new ApplicationException();
            }
        }
    }
}