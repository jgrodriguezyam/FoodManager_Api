﻿using FoodManager.DTO;
using FoodManager.DTO.BaseRequest;
using FoodManager.DTO.BaseResponse;
using FoodManager.DTO.Message.Workers;

namespace FoodManager.Services.Interfaces
{
    public interface IWorkerService
    {
        FindWorkersResponse Find(FindWorkersRequest request);
        CreateResponse Create(WorkerRequest request);
        SuccessResponse Update(WorkerRequest request);
        Worker Get(GetWorkerRequest request);
        SuccessResponse Delete(DeleteWorkerRequest request);
        SuccessResponse ChangeStatus(ChangeStatusRequest request);
    }
}