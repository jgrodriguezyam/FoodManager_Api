using System.Collections.Generic;
using System.IO;
using System.Net;
using FastMapper;
using FoodManager.DTO.BaseRequest;
using FoodManager.DTO.BaseResponse;
using FoodManager.DTO.Message.SaucerMultimedias;
using FoodManager.Infrastructure.Exceptions;
using FoodManager.Infrastructure.Files;
using FoodManager.Model;
using FoodManager.Model.IRepositories;
using FoodManager.Queries.SaucerMultimedias;
using FoodManager.Services.Interfaces;
using FoodManager.Services.Validators.Interfaces;
using File = FoodManager.Infrastructure.Files.File;

namespace FoodManager.Services.Implements
{
    public class SaucerMultimediaService : ISaucerMultimediaService
    {
        private readonly ISaucerMultimediaQuery _saucerMultimediaQuery;
        private readonly ISaucerMultimediaRepository _saucerMultimediaRepository;
        private readonly ISaucerMultimediaValidator _saucerMultimediaValidator;
        private readonly IStorageProvider _storageProvider;
        private readonly IFileValidator _fileValidator;

        public SaucerMultimediaService(ISaucerMultimediaQuery saucerMultimediaQuery, ISaucerMultimediaRepository saucerMultimediaRepository, ISaucerMultimediaValidator saucerMultimediaValidator, IStorageProvider storageProvider, IFileValidator fileValidator)
        {
            _saucerMultimediaQuery = saucerMultimediaQuery;
            _saucerMultimediaRepository = saucerMultimediaRepository;
            _saucerMultimediaValidator = saucerMultimediaValidator;
            _storageProvider = storageProvider;
            _fileValidator = fileValidator;
        }

        public FindSaucerMultimediasResponse Find(FindSaucerMultimediasRequest request)
        {
            try
            {
                _saucerMultimediaQuery.WithOnlyActivated(true);
                _saucerMultimediaQuery.WithOnlyStatusActivated(request.OnlyStatusActivated);
                _saucerMultimediaQuery.WithOnlyStatusDeactivated(request.OnlyStatusDeactivated);
                _saucerMultimediaQuery.WithSaucer(request.SaucerId);
                _saucerMultimediaQuery.Sort(request.Sort, request.SortBy);
                var totalRecords = _saucerMultimediaQuery.TotalRecords();
                _saucerMultimediaQuery.Paginate(request.StartPage, request.EndPage);
                var saucerMultimedias = _saucerMultimediaQuery.Execute();

                return new FindSaucerMultimediasResponse
                {
                    SaucerMultimedias = TypeAdapter.Adapt<List<SaucerMultimediaResponse>>(saucerMultimedias),
                    TotalRecords = totalRecords
                };
            }
            catch (DataAccessException)
            {
                throw new ApplicationException();
            }
        }

        public CreateResponse Create(SaucerMultimediaRequest request, File file)
        {
            try
            {
                var saucerMultimedia = TypeAdapter.Adapt<SaucerMultimedia>(request);
                _saucerMultimediaValidator.ValidateAndThrowException(saucerMultimedia, "Base");
                _fileValidator.ValidateAndThrowException(file, "Base");
                var savedFilePath = _storageProvider.Save(file);
                saucerMultimedia.Path = savedFilePath;
                _saucerMultimediaRepository.Add(saucerMultimedia);
                return new CreateResponse(saucerMultimedia.Id);
            }
            catch (DataAccessException)
            {
                throw new ApplicationException();
            }
        }

        public SuccessResponse Update(SaucerMultimediaRequest request)
        {
            try
            {
                var currentSaucerMultimedia = _saucerMultimediaRepository.FindBy(request.Id);
                currentSaucerMultimedia.ThrowExceptionIfIsNull("Multimedia de platillo no encontrado");
                var saucerMultimediaToCopy = TypeAdapter.Adapt<SaucerMultimedia>(request);
                TypeAdapter.Adapt(saucerMultimediaToCopy, currentSaucerMultimedia);
                _saucerMultimediaValidator.ValidateAndThrowException(currentSaucerMultimedia, "Base");
                _saucerMultimediaRepository.Update(currentSaucerMultimedia);
                return new SuccessResponse { IsSuccess = true };
            }
            catch (DataAccessException)
            {
                throw new ApplicationException();
            }
        }

        public DTO.SaucerMultimedia Get(GetSaucerMultimediaRequest request)
        {
            try
            {
                var saucerMultimedia = _saucerMultimediaRepository.FindBy(request.Id);
                saucerMultimedia.ThrowExceptionIfIsNull("Multimedia de platillo no encontrado");
                return TypeAdapter.Adapt<DTO.SaucerMultimedia>(saucerMultimedia);
            }
            catch (DataAccessException)
            {
                throw new ApplicationException();
            }
        }

        public SuccessResponse Delete(DeleteSaucerMultimediaRequest request)
        {
            try
            {
                var saucerMultimedia = _saucerMultimediaRepository.FindBy(request.Id);
                saucerMultimedia.ThrowExceptionIfIsNull("Multimedia de platillo no encontrado");
                _saucerMultimediaRepository.Remove(saucerMultimedia);
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
                var saucerMultimedia = _saucerMultimediaRepository.FindBy(request.Id);
                saucerMultimedia.ThrowExceptionIfIsNull("Multimedia de platillo no encontrado");
                if (saucerMultimedia.Status.Equals(request.Status))
                    ExceptionExtensions.ThrowStatusException(HttpStatusCode.Accepted, request.Status);
                saucerMultimedia.Status = request.Status;
                _saucerMultimediaRepository.Update(saucerMultimedia);
                return new SuccessResponse { IsSuccess = true };
            }
            catch (DataAccessException)
            {
                throw new ApplicationException();
            }
        }

        public Stream GetFile(GetFileRequest request)
        {
            try
            {
                var saucerMultimedia = _saucerMultimediaRepository.FindBy(request.Id);
                saucerMultimedia.ThrowExceptionIfIsNull("Multimedia de platillo no encontrado");
                var stream = _storageProvider.Retrieve(saucerMultimedia.Path);
                return stream;
            }
            catch (DataAccessException)
            {
                throw new ApplicationException();
            }
        }
    }
}