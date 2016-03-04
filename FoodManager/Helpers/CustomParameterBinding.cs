using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Metadata;
using Newtonsoft.Json;

namespace FoodManager.Helpers
{
    public class CustomParameterBinding : HttpParameterBinding
    {
        private readonly JsonSerializerSettings _jsonSerializerSettings;

        public override bool WillReadBody
        {
            get
            {
                return !Descriptor.ActionDescriptor.SupportedHttpMethods.Contains(HttpMethod.Get);
            }
        }

        public CustomParameterBinding(HttpParameterDescriptor descriptor) : base(descriptor)
        {
            _jsonSerializerSettings = descriptor.Configuration.Formatters.JsonFormatter.SerializerSettings;
        }

        public override async Task ExecuteBindingAsync(ModelMetadataProvider metadataProvider, HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            var properties = actionContext.RequestContext.RouteData.Values.ToDictionary(value => value.Key, value => value.Value);

            if (actionContext.Request.Method == HttpMethod.Get)
            {
                foreach (var pair in actionContext.Request.GetQueryNameValuePairs().Where(pair => !properties.ContainsKey(pair.Key)))
                {
                    properties.Add(pair.Key, pair.Value);
                }
            }
            else
            {
                try
                {
                    string content = await actionContext.Request.Content.ReadAsStringAsync();
                    var contentAsDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(content, _jsonSerializerSettings);
                    foreach (var pair in contentAsDictionary.Where(pair => !properties.ContainsKey(pair.Key)))
                    {
                        properties.Add(pair.Key, pair.Value);
                    }
                }
                catch (Exception)
                {
                }
            }

            var json = JsonConvert.SerializeObject(properties, _jsonSerializerSettings);
            var model = JsonConvert.DeserializeObject(json, Descriptor.ParameterType, _jsonSerializerSettings);
            actionContext.ActionArguments.Add(Descriptor.ParameterName, model);
        }
    }
}