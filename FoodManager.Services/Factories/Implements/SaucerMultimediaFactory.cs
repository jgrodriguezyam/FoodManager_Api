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
            return AppendProperties(new[] { saucerMultimedia }).FirstOrDefault();
        }

        public IEnumerable<SaucerMultimediaResponse> Execute(IEnumerable<SaucerMultimedia> saucerMultimedias)
        {
            return AppendProperties(saucerMultimedias);
        }

        private IEnumerable<SaucerMultimediaResponse> AppendProperties(IEnumerable<SaucerMultimedia> saucerMultimedias)
        {
            var saucerMultimediasResponse = TypeAdapter.Adapt<List<SaucerMultimediaResponse>>(saucerMultimedias);
            var saucers = _saucerRepository.FindBy(saucer => saucer.IsActive);

            saucerMultimediasResponse.ForEach(saucerMultimediaResponse =>
            {
                var saucerMultimedia = saucerMultimedias.First(saucerMultimediaModel => saucerMultimediaModel.Id == saucerMultimediaResponse.Id);
                var saucer = saucers.First(saucerModel => saucerModel.Id == saucerMultimedia.SaucerId);
                saucerMultimediaResponse.Saucer = TypeAdapter.Adapt<SaucerResponse>(saucer);
            });

            return saucerMultimediasResponse;
        }
    }
}