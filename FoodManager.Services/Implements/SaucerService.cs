﻿using System.Collections.Generic;
using System.Net;
using FastMapper;
using FoodManager.DTO.BaseRequest;
using FoodManager.DTO.BaseResponse;
using FoodManager.DTO.Message.Saucers;
using FoodManager.Infrastructure.Exceptions;
using FoodManager.Model;
using FoodManager.Model.IRepositories;
using FoodManager.Queries.Saucers;
using FoodManager.Services.Interfaces;
using FoodManager.Services.Validators.Interfaces;

namespace FoodManager.Services.Implements
{
    public class SaucerService : ISaucerService
    {
        private readonly ISaucerQuery _saucerQuery;
        private readonly ISaucerRepository _saucerRepository;
        private readonly ISaucerValidator _saucerValidator;

        public SaucerService(ISaucerQuery saucerQuery, ISaucerRepository saucerRepository, ISaucerValidator saucerValidator)
        {
            _saucerQuery = saucerQuery;
            _saucerRepository = saucerRepository;
            _saucerValidator = saucerValidator;
        }

        public FindSaucersResponse Find(FindSaucersRequest request)
        {
            try
            {
                _saucerQuery.WithOnlyActivated(true);
                _saucerQuery.WithOnlyStatusActivated(request.OnlyStatusActivated);
                _saucerQuery.WithOnlyStatusDeactivated(request.OnlyStatusDeactivated);
                _saucerQuery.Sort(request.Sort, request.SortBy);
                var totalRecords = _saucerQuery.TotalRecords();
                _saucerQuery.Paginate(request.StartPage, request.EndPage);
                var saucers = _saucerQuery.Execute();

                return new FindSaucersResponse
                {
                    Saucers = TypeAdapter.Adapt<List<SaucerResponse>>(saucers),
                    TotalRecords = totalRecords
                };
            }
            catch (DataAccessException)
            {
                throw new ApplicationException();
            }
        }

        public CreateResponse Create(SaucerRequest request)
        {
            try
            {
                var saucer = TypeAdapter.Adapt<Saucer>(request);
                _saucerValidator.ValidateAndThrowException(saucer, "Base");
                _saucerRepository.Add(saucer);
                return new CreateResponse(saucer.Id);
            }
            catch (DataAccessException)
            {
                throw new ApplicationException();
            }
        }

        public SuccessResponse Update(SaucerRequest request)
        {
            try
            {
                var currentSaucer = _saucerRepository.FindBy(request.Id);
                currentSaucer.ThrowExceptionIfIsNull("Platillo no encontrado");
                var saucerToCopy = TypeAdapter.Adapt<Saucer>(request);
                TypeAdapter.Adapt(saucerToCopy, currentSaucer);
                _saucerValidator.ValidateAndThrowException(currentSaucer, "Base");
                _saucerRepository.Update(currentSaucer);
                return new SuccessResponse { IsSuccess = true };
            }
            catch (DataAccessException)
            {
                throw new ApplicationException();
            }
        }

        public DTO.Saucer Get(GetSaucerRequest request)
        {
            try
            {
                var saucer = _saucerRepository.FindBy(request.Id);
                saucer.ThrowExceptionIfIsNull("Platillo no encontrado");
                return TypeAdapter.Adapt<DTO.Saucer>(saucer);
            }
            catch (DataAccessException)
            {
                throw new ApplicationException();
            }
        }

        public SuccessResponse Delete(DeleteSaucerRequest request)
        {
            try
            {
                var saucer = _saucerRepository.FindBy(request.Id);
                saucer.ThrowExceptionIfIsNull("Platillo no encontrado");
                _saucerRepository.Remove(saucer);
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
                var saucer = _saucerRepository.FindBy(request.Id);
                saucer.ThrowExceptionIfIsNull("Platillo no encontrado");
                if (saucer.Status.Equals(request.Status))
                    ExceptionExtensions.ThrowStatusException(HttpStatusCode.Accepted, request.Status);
                saucer.Status = request.Status;
                _saucerRepository.Update(saucer);
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
                var isReference = _saucerRepository.IsReference(request.Id);
                return new SuccessResponse { IsSuccess = isReference };
            }
            catch (DataAccessException)
            {
                throw new ApplicationException();
            }
        }
    }
}