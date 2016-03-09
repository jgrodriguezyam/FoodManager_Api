﻿using System.Web.Http;
using FoodManager.DTO;
using FoodManager.DTO.BaseResponse;
using FoodManager.DTO.Message.Warnings;
using FoodManager.Services.Interfaces;

namespace FoodManager.Controllers
{
    public class WarningController : ApiController
    {
        private readonly IWarningService _warningService;

        public WarningController(IWarningService warningService)
        {
            _warningService = warningService;
        }

        [HttpGet, Route("warnings")]
        public FindWarningsResponse Get(FindWarningsRequest request)
        {
            return _warningService.Find(request);
        }

        [HttpPost, Route("warnings")]
        public CreateResponse Post(WarningRequest request)
        {
            return _warningService.Create(request);
        }

        [HttpPut, Route("warnings")]
        public SuccessResponse Put(WarningRequest request)
        {
            return _warningService.Update(request);
        }

        [HttpGet, Route("warnings/{Id}")]
        public Warning Get(GetWarningRequest request)
        {
            return _warningService.Get(request);
        }

        [HttpDelete, Route("warnings/{Id}")]
        public SuccessResponse Delete(DeleteWarningRequest request)
        {
            return _warningService.Delete(request);
        }
    }
}