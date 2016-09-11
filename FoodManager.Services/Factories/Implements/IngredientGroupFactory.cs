using System.Collections.Generic;
using System.Linq;
using FastMapper;
using FoodManager.DTO.Message.IngredientGroups;
using FoodManager.Infrastructure.Files;
using FoodManager.Infrastructure.Integers;
using FoodManager.Model;
using FoodManager.Model.IRepositories;
using FoodManager.Services.Factories.Interfaces;
using ServiceStack.Common.Extensions;

namespace FoodManager.Services.Factories.Implements
{
    public class IngredientGroupFactory : IIngredientGroupFactory
    {
        private readonly IStorageProvider _storageProvider;
        private readonly IIngredientRepository _ingredientRepository;

        public IngredientGroupFactory(IStorageProvider storageProvider, IIngredientRepository ingredientRepository)
        {
            _storageProvider = storageProvider;
            _ingredientRepository = ingredientRepository;
        }

        public IngredientGroupResponse Execute(IngredientGroup ingredientGroup)
        {
            return AppendProperties(new[] { ingredientGroup }).FirstOrDefault();
        }

        public IEnumerable<IngredientGroupResponse> Execute(IEnumerable<IngredientGroup> ingredientGroups)
        {
            return AppendProperties(ingredientGroups);
        }

        private IEnumerable<IngredientGroupResponse> AppendProperties(IEnumerable<IngredientGroup> ingredientGroups)
        {
            var ingredientGroupsResponse = TypeAdapter.Adapt<List<IngredientGroupResponse>>(ingredientGroups);
            var ingredients = _ingredientRepository.FindBy(ingredient => ingredient.IsActive);

            ingredientGroupsResponse.ForEach(ingredientGroupResponse =>
            {
                var ingredientGroup = ingredientGroups.First(ingredientGroupModel => ingredientGroupModel.Id == ingredientGroupResponse.Id);
                var amountOfReferences = ingredients.Count(ingredient => ingredient.IngredientGroupId == ingredientGroup.Id);
                ingredientGroupResponse.IsReference = amountOfReferences.IsNotZero();
            });

            return ingredientGroupsResponse;
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