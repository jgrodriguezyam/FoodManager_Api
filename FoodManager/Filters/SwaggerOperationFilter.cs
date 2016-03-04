using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http.Description;
using FoodManager.Infrastructure.Collections;
using Swashbuckle.Swagger;

namespace FoodManager.Filters
{
    public class SwaggerOperationFilter : IOperationFilter
    {
        public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
        {
            if (apiDescription.HttpMethod == HttpMethod.Get && operation.parameters.IsNotEmpty())
            {
                var parameters = new List<Parameter>();
                foreach (var parameter in operation.parameters)
                {
                    if (parameter.@in == "body")
                    {
                        var parameterDescription = apiDescription.ParameterDescriptions.First(description => description.Name == parameter.name);
                        var parameterType = parameterDescription.ParameterDescriptor.ParameterType;
                        var schema = schemaRegistry.Definitions[parameterType.Name];
                        foreach (var property in schema.properties.Where(property => operation.parameters.All(operationParameter => operationParameter.name != property.Key)))
                        {
                            parameters.Add(new Parameter { @in = "query", name = property.Key, type = property.Value.type });
                        }
                    }
                    else
                    {
                        parameters.Add(parameter);
                    }
                    operation.parameters = parameters;
                }
            }
        }
    }
}