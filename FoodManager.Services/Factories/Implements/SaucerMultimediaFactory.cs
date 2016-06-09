using System.Collections.Generic;
using System.Linq;
using FastMapper;
using FoodManager.DTO.Message.SaucerMultimedias;
using FoodManager.DTO.Message.Saucers;
using FoodManager.Model;
using FoodManager.Model.IRepositories;
using FoodManager.Services.Factories.Interfaces;

namespace FoodManager.Services.Factories.Implements
{
    public class SaucerMultimediaFactory : ISaucerMultimediaFactory
    {
        private readonly ISaucerRepository _saucerRepository;

        public SaucerMultimediaFactory(ISaucerRepository saucerRepository)
        {
            _saucerRepository = saucerRepository;
        }

        public SaucerMultimediaResponse Execute(SaucerMultimedia saucerMultimedia)
        {
            var saucerMultimediaResponse = TypeAdapter.Adapt<SaucerMultimediaResponse>(saucerMultimedia);
            var saucer = _saucerRepository.FindBy(saucerMultimedia.SaucerId);
            saucerMultimediaResponse.Saucer = TypeAdapter.Adapt<SaucerResponse>(saucer);
            return saucerMultimediaResponse;
        }

        public IEnumerable<SaucerMultimediaResponse> Execute(IEnumerable<SaucerMultimedia> saucerMultimedias)
        {
            return saucerMultimedias.Select(Execute);
        }
    }
}