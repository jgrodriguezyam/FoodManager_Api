using System;
using System.Linq;
using System.Web.Script.Serialization;
using FoodManager.DTO.BaseResponse;
using FoodManager.Infrastructure.Objects;
using FoodManager.Infrastructure.Strings;
using Newtonsoft.Json;
using RestSharp;

namespace FoodManager.Client.ClientSettings
{
    public static class Client
    {
        public static TModelResponse Get<TModelResponse>(string uri)
        {
            var restRequest = new RestRequest(uri, Method.GET);
            return ExcecuteRestClient<TModelResponse>(restRequest);
        }

        public static TModelResponse Filter<TModelResponse>(string uri, Object request)
        {
            var restRequest = new RestRequest(uri, Method.GET);
            var dictionary = request.ConvertToDictionary();

            if (dictionary.IsNull())
                return ExcecuteRestClient<TModelResponse>(restRequest);
            
                foreach (var objectParameter in dictionary.Where(parameter => parameter.Value.IsNotNullOrEmpty()))
                    restRequest.AddParameter(objectParameter.Key, objectParameter.Value);

            return ExcecuteRestClient<TModelResponse>(restRequest);
        }

        public static TModelResponse Filter<TModelResponse>(string uri)
        {
            var restRequest = new RestRequest(uri, Method.GET);
            return ExcecuteRestClient<TModelResponse>(restRequest);
        }

        public static CreateResponse Post(string uri, Object objectBody)
        {
            var restRequest = new RestRequest(uri, Method.POST);

            if (objectBody.IsNotNull())
                restRequest.AddParameter("application/json", new JavaScriptSerializer().Serialize(objectBody), ParameterType.RequestBody);
            return ExcecuteRestClient<CreateResponse>(restRequest);
        }

        public static CreateResponse Post(string uri)
        {
            var restRequest = new RestRequest(uri, Method.POST);
            return ExcecuteRestClient<CreateResponse>(restRequest);
        }

        public static SuccessResponse Put(string uri, Object objectBody)
        {
            var restRequest = new RestRequest(uri, Method.PUT);

            if (objectBody.IsNotNull())
                restRequest.AddParameter("application/json", new JavaScriptSerializer().Serialize(objectBody), ParameterType.RequestBody);
            return ExcecuteRestClient<SuccessResponse>(restRequest);
        }

        public static SuccessResponse Delete(string uri)
        {
            var restRequest = new RestRequest(uri, Method.DELETE);
            return ExcecuteRestClient<SuccessResponse>(restRequest);
        }

        public static SuccessResponse Assign(string uri)
        {
            var restRequest = new RestRequest(uri, Method.POST);
            return ExcecuteRestClient<SuccessResponse>(restRequest);
        }

        public static SuccessResponse AssignMany(string uri, Object objectBody)
        {
            var restRequest = new RestRequest(uri, Method.PUT);
            if (objectBody.IsNotNull())
                restRequest.AddParameter("application/json", new JavaScriptSerializer().Serialize(objectBody), ParameterType.RequestBody);
            return ExcecuteRestClient<SuccessResponse>(restRequest);
        }

        public static SuccessResponse UnAssign(string uri)
        {
            var restRequest = new RestRequest(uri, Method.DELETE);
            return ExcecuteRestClient<SuccessResponse>(restRequest);
        }

        public static SuccessResponse UnAssignMany(string uri, Object objectBody)
        {
            var restRequest = new RestRequest(uri, Method.DELETE);
            if (objectBody.IsNotNull())
                restRequest.AddParameter("application/json", new JavaScriptSerializer().Serialize(objectBody), ParameterType.RequestBody);
            return ExcecuteRestClient<SuccessResponse>(restRequest);
        }

        private static TModelResponse ExcecuteRestClient<TModelResponse>(IRestRequest restRequest)
        {
            var restClient = new RestClient {BaseUrl = new Uri(ClientConnection.ServerApi)};
            restRequest.AddHeader("Content-Type", "application/json");
            var response = restClient.Execute(restRequest);
            ClientValidate.ThrowIfNotSuccess(response.StatusCode);
            
            var responseContent = response.Content;
            var model = JsonConvert.DeserializeObject<TModelResponse>(responseContent);
            return model;
        }
    }
}