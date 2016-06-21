using System.Collections.Generic;
using System.Linq;
using FastMapper;
using FoodManager.DTO.Message.IngredientGroups;
using FoodManager.Infrastructure.Files;
using FoodManager.Model;
using FoodManager.Model.IRepositories;
using FoodManager.Services.Factories.Interfaces;
using ServiceStack.Common.Extensions;

namespace FoodManager.Services.Factories.Implements
{
    public class IngredientGroupFactory : IIngredientGroupFactory
    {
        private readonly IStorageProvider _storageProvider;
        private readonly IIngredientGroupRepository _ingredientGroupRepository;

        public IngredientGroupFactory(IStorageProvider storageProvider, IIngredientGroupRepository ingredientGroupRepository)
        {
            _storageProvider = storageProvider;
            _ingredientGroupRepository = ingredientGroupRepository;
        }

        public IngredientGroupResponse Execute(IngredientGroup ingredientGroup)
        {
            var ingredientGroupResponse = TypeAdapter.Adapt<IngredientGroupResponse>(ingredientGroup);
            ingredientGroupResponse.IsReference = _ingredientGroupRepository.IsReference(ingredientGroup.Id);
            return ingredientGroupResponse;
        }

        public IEnumerable<IngredientGroupResponse> Execute(IEnumerable<IngredientGroup> ingredientGroups)
        {
            return ingredientGroups.Select(Execute);
        }

        public List<IngredientGroup> FromCsv(string fileName)
        {
            var ingredientGroups = new List<IngredientGroup>();
            var csvLines = _storageProvider.ReadAllLinesCsv(fileName);
            csvLines.ForEach(csvLine =>
                             {
                             var values = csvLine.Split(',');
                             ingredientGroups.Add(new IngredientGroup
                                 {
                                     Name = values[0],
                                     Color = values[1]
                                 });
                             });
                                           
            return ingredientGroups;
        }
    }
}