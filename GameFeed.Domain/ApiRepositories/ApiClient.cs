using System;
using System.Collections.Generic;
using System.Linq;
using GameFeed.Common;
using Newtonsoft.Json;
using RestSharp;

namespace GameFeed.Domain.ApiRepositories {

    public interface IApiClient {
        T Get<T>(string endpoint) where T : new();
        IEnumerable<T> GetMultiple<T>(string endpoint);
    }

    public class ApiClient : IApiClient {

        /// <summary>
        /// Returns the requested object of type 'T' from the API
        /// </summary>
        /// <typeparam name="T">The return type</typeparam>
        /// <param name="endpoint">The endpoint after the base URL</param>
        /// <returns>The returned object of type 'T'</returns>
        public T Get<T>(string endpoint) where T : new() {
            IRestResponse response = ExecuteRequest(endpoint);

            //Deserialize JSON-data to the required type
            return JsonConvert.DeserializeObject<List<T>>(response.Content).FirstOrDefault();
        }

        /// <summary>
        /// Returns a list of the requested objects of type 'T' from the API
        /// </summary>
        /// <typeparam name="T">The return type</typeparam>
        /// <param name="endpoint">The endpoint after the base URL</param>
        /// <returns>The returned array with objects of type 'T'</returns>
        public IEnumerable<T> GetMultiple<T>(string endpoint) {
            IRestResponse response = ExecuteRequest(endpoint);

            //Deserialize JSON-data to an array of the required type
            return JsonConvert.DeserializeObject<List<T>>(response.Content);
        }

        /// <summary>
        /// Executes a REST request to the given endpoint
        /// </summary>
        /// <param name="endPoint"></param>
        /// <returns></returns>
        private static IRestResponse ExecuteRequest(string endPoint) {
            //Setup request
            RestClient client = new RestClient { BaseUrl = new Uri(Settings.IgdbApiBaseUrl) };

            RestRequest request = new RestRequest();
            request.AddHeader("user-key", Settings.IgdbApiKey);
            request.Resource = endPoint;

            return client.Execute(request);
        }
    }
}
