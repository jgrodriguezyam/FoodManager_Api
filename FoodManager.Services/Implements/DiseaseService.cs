using System.Collections.Generic;
using System.Net;
using FastMapper;
using FoodManager.DTO.BaseRequest;
using FoodManager.DTO.BaseResponse;
using FoodManager.DTO.Message.Diseases;
using FoodManager.Infrastructure.Exceptions;
using FoodManager.Model;
using FoodManager.Model.IRepositories;
using FoodManager.Queries.Diseases;
using FoodManager.Services.Interfaces;
using FoodManager.Services.Validators.Interfaces;

namespace FoodManager.Services.Implements
{
    public class DiseaseService : IDiseaseService
    {
        private readonly IDiseaseQuery _diseaseQuery;
        private readonly IDiseaseRepository _diseaseRepository;
        private readonly IDiseaseValidator _diseaseValidator;

        public DiseaseService(IDiseaseQuery diseaseQuery, IDiseaseRepository diseaseRepository, IDiseaseValidator diseaseValidator)
        {
            _diseaseQuery = diseaseQuery;
            _diseaseRepository = diseaseRepository;
            _diseaseValidator = diseaseValidator;
        }

        public FindDiseasesResponse Find(FindDiseasesRequest request)
        {
            try
            {
                _diseaseQuery.WithOnlyActivated(true);
                _diseaseQuery.WithOnlyStatusActivated(request.OnlyStatusActivated);
                _diseaseQuery.WithOnlyStatusDeactivated(request.OnlyStatusDeactivated);
                _diseaseQuery.Sort(request.Sort, request.SortBy);
                var totalRecords = _diseaseQuery.TotalRecords();
                _diseaseQuery.Paginate(request.StartPage, request.EndPage);
                var regions = _diseaseQuery.Execute();

                return new FindDiseasesResponse
                {
                    Diseases = TypeAdapter.Adapt<List<DiseaseResponse>>(regions),
                    TotalRecords = totalRecords
                };
            }
            catch (DataAccessException)
            {
                throw new ApplicationException();
            }
        }

        public CreateResponse Create(DiseaseRequest request)
        {
            try
            {
                var disease = TypeAdapter.Adapt<Disease>(request);
                _diseaseValidator.ValidateAndThrowException(disease, "Base");
                _diseaseRepository.Add(disease);
                return new CreateResponse(disease.Id);
            }
            catch (DataAccessException)
            {
                throw new ApplicationException();
            }
        }

        public SuccessResponse Update(DiseaseRequest request)
        {
            try
            {
                var currentDisease = _diseaseRepository.FindBy(request.Id);
                currentDisease.ThrowExceptionIfIsNull("Enfermedad no encontrada");
                var diseasToCopy = TypeAdapter.Adapt<Disease>(request);
                TypeAdapter.Adapt(diseasToCopy, currentDisease);
                _diseaseValidator.ValidateAndThrowException(currentDisease, "Base");
                _diseaseRepository.Update(currentDisease);
                return new SuccessResponse { IsSuccess = true };
            }
            catch (DataAccessException)
            {
                throw new ApplicationException();
            }
        }

        public DTO.Disease Get(GetDiseaseRequest request)
        {
            try
            {
                var disease = _diseaseRepository.FindBy(request.Id);
                disease.ThrowExceptionIfIsNull("Enfermedad no encontrada");
                return TypeAdapter.Adapt<DTO.Disease>(disease);
            }
            catch (DataAccessException)
            {
                throw new ApplicationException();
            }
        }

        public SuccessResponse Delete(DeleteDiseaseRequest request)
        {
            try
            {
                var disease = _diseaseRepository.FindBy(request.Id);
                disease.ThrowExceptionIfIsNull("Enfermedad no encontrada");
                var isReference = _diseaseRepository.IsReference(request.Id);
                if (isReference)
                    ExceptionExtensions.ThrowIsReferenceException();
                _diseaseRepository.Remove(disease);
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
                var disease = _diseaseRepository.FindBy(request.Id);
                disease.ThrowExceptionIfIsNull("Enfermedad no encontrada");
                if (disease.Status.Equals(request.Status))
                    ExceptionExtensions.ThrowStatusException(HttpStatusCode.Accepted, request.Status);
                disease.Status = request.Status;
                _diseaseRepository.Update(disease);
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
                var isReference = _diseaseRepository.IsReference(request.Id);
                return new SuccessResponse { IsSuccess = isReference };
            }
            catch (DataAccessException)
            {
                throw new ApplicationException();
            }
        }
    }
}