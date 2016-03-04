using System.Collections.Generic;
using FastMapper;
using FoodManager.Services.Interfaces;
using FoodManager.DTO.BaseResponse;
using FoodManager.DTO.Message.Regions;
using FoodManager.Infrastructure.Exceptions;
using FoodManager.Model;
using FoodManager.Model.IRepositories;
using FoodManager.Queries.Regions;
using FoodManager.Services.Validators.Interfaces;

namespace FoodManager.Services.Implements
{
    public class RegionService : IRegionService
    {
        private readonly IRegionQuery _regionQuery;
        private readonly IRegionRepository _regionRepository;
        private readonly IRegionValidator _regionValidator;

        public RegionService(IRegionQuery regionQuery, IRegionRepository regionRepository, IRegionValidator regionValidator)
        {
            _regionQuery = regionQuery;
            _regionRepository = regionRepository;
            _regionValidator = regionValidator;
        }
        
        public FindRegionsResponse Find(FindRegionsRequest request)
        {
            try
            {
                _regionQuery.WithOnlyActivated(true);
                _regionQuery.Sort(request.Sort, request.SortBy);
                var totalRecords = _regionQuery.TotalRecords();
                _regionQuery.Paginate(request.StartPage, request.EndPage);
                var regions = _regionQuery.Execute();

                return new FindRegionsResponse
                {
                    Regions = TypeAdapter.Adapt<List<RegionResponse>>(regions),
                    TotalRecords = totalRecords
                };
            }
            catch (DataAccessException)
            {
                throw new ApplicationException();
            }
        }

        public CreateResponse Create(RegionRequest request)
        {
            try
            {
                var regionToCreate = TypeAdapter.Adapt<Region>(request);
                _regionValidator.ValidateAndThrowException(regionToCreate, "Base");
                _regionRepository.Add(regionToCreate);
                return new CreateResponse(regionToCreate.Id);
            }
            catch (DataAccessException)
            {
                throw new ApplicationException();
            }
        }

        public SuccessResponse Update(RegionRequest request)
        {
            try
            {
                var currentRegion = _regionRepository.FindBy(request.Id);
                currentRegion.ThrowExceptionIfIsNull("Region no encontrada");
                var regionToCopy = TypeAdapter.Adapt<Region>(request);
                TypeAdapter.Adapt(regionToCopy, currentRegion);
                _regionValidator.ValidateAndThrowException(currentRegion, "Base");
                _regionRepository.Update(currentRegion);
                return new SuccessResponse { IsSuccess = true };
            }
            catch (DataAccessException)
            {
                throw new ApplicationException();
            }
        }

        public DTO.Region Get(GetRegionRequest request)
        {
            try
            {
                var region = _regionRepository.FindBy(request.Id);
                region.ThrowExceptionIfIsNull("Region no encontrada");
                return TypeAdapter.Adapt<DTO.Region>(region);
            }
            catch (DataAccessException)
            {
                throw new ApplicationException();
            }
        }

        public SuccessResponse Delete(DeleteRegionRequest request)
        {
            try
            {
                var region = _regionRepository.FindBy(request.Id);
                region.ThrowExceptionIfIsNull("Region no encontrada");
                _regionRepository.Remove(region);
                return new SuccessResponse { IsSuccess = true };
            }
            catch (DataAccessException)
            {
                throw new ApplicationException();
            }
        }
    }
}