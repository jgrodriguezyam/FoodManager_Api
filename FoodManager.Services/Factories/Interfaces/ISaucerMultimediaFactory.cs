using System.Collections.Generic;
using FoodManager.DTO.Message.SaucerMultimedias;
using FoodManager.Model;

namespace FoodManager.Services.Factories.Interfaces
{
    public interface ISaucerMultimediaFactory
    {
        SaucerMultimediaResponse Execute(SaucerMultimedia saucerMultimedia);
        IEnumerable<SaucerMultimediaResponse> Execute(IEnumerable<SaucerMultimedia> saucerMultimedias);
    }
}