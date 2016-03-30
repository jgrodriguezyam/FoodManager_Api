using System.Collections.Generic;
using System.Net;
using FastMapper;
using FoodManager.DTO.BaseRequest;
using FoodManager.DTO.BaseResponse;
using FoodManager.DTO.Message.Companies;
using FoodManager.Infrastructure.Exceptions;
using FoodManager.Model;
using FoodManager.Model.IRepositories;
using FoodManager.Queries.Companies;
using FoodManager.Services.Factories.Interfaces;
using FoodManager.Services.Interfaces;
using FoodManager.Services.Validators.Interfaces;

namespace FoodManager.Services.Implements
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyQuery _companyQuery;
        private readonly ICompanyRepository _companyRepository;
        private readonly ICompanyValidator _companyValidator;
        private readonly ICompanyFactory _companyFactory;

        public CompanyService(ICompanyQuery companyQuery, ICompanyRepository companyRepository, ICompanyValidator companyValidator, ICompanyFactory companyFactory)
        {
            _companyQuery = companyQuery;
            _companyRepository = companyRepository;
            _companyValidator = companyValidator;
            _companyFactory = companyFactory;
        }

        public FindCompaniesResponse Find(FindCompaniesRequest request)
        {
            try
            {
                _companyQuery.WithOnlyActivated(true);
                _companyQuery.WithOnlyStatusActivated(request.OnlyStatusActivated);
                _companyQuery.WithOnlyStatusDeactivated(request.OnlyStatusDeactivated);
                _companyQuery.WithRegion(request.RegionId);
                _companyQuery.WithName(request.Name);
                _companyQuery.Sort(request.Sort, request.SortBy);
                var totalRecords = _companyQuery.TotalRecords();
                _companyQuery.Paginate(request.StartPage, request.EndPage);
                var companies = _companyQuery.Execute();

                return new FindCompaniesResponse
                {
                    Companies = TypeAdapter.Adapt<List<CompanyResponse>>(companies),
                    TotalRecords = totalRecords
                };
            }
            catch (DataAccessException)
            {
                throw new ApplicationException();
            }
        }

        public CreateResponse Create(CompanyRequest request)
        {
            try
            {
                var company = TypeAdapter.Adapt<Company>(request);
                _companyValidator.ValidateAndThrowException(company, "Base");
                _companyRepository.Add(company);
                return new CreateResponse(company.Id);
            }
            catch (DataAccessException)
            {
                throw new ApplicationException();
            }
        }

        public SuccessResponse Update(CompanyRequest request)
        {
            try
            {
                var currentCompany = _companyRepository.FindBy(request.Id);
                currentCompany.ThrowExceptionIfIsNull("Compania no encontrada");
                var copanyToCopy = TypeAdapter.Adapt<Company>(request);
                TypeAdapter.Adapt(copanyToCopy, currentCompany);
                _companyValidator.ValidateAndThrowException(currentCompany, "Base");
                _companyRepository.Update(currentCompany);
                return new SuccessResponse { IsSuccess = true };
            }
            catch (DataAccessException)
            {
                throw new ApplicationException();
            }
        }

        public DTO.Company Get(GetCompanyRequest request)
        {
            try
            {
                var company = _companyRepository.FindBy(request.Id);
                company.ThrowExceptionIfIsNull("Compania no encontrada");
                return _companyFactory.Execute(company);
            }
            catch (DataAccessException)
            {
                throw new ApplicationException();
            }
        }

        public SuccessResponse Delete(DeleteCompanyRequest request)
        {
            try
            {
                var company = _companyRepository.FindBy(request.Id);
                company.ThrowExceptionIfIsNull("Compania no encontrada");
                var isReference = _companyRepository.IsReference(request.Id);
                if (isReference)
                    ExceptionExtensions.ThrowIsReferenceException();
                _companyRepository.Remove(company);
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
                var company = _companyRepository.FindBy(request.Id);
                company.ThrowExceptionIfIsNull("Compania no encontrada");
                if (company.Status.Equals(request.Status))
                    ExceptionExtensions.ThrowStatusException(HttpStatusCode.Accepted, request.Status);
                company.Status = request.Status;
                _companyRepository.Update(company);
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
                var isReference = _companyRepository.IsReference(request.Id);
                return new SuccessResponse { IsSuccess = isReference };
            }
            catch (DataAccessException)
            {
                throw new ApplicationException();
            }
        }
    }
}