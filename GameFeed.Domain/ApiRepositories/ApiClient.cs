using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using GameFeed.Common;
using GameFeed.Common.Exceptions;
using GameFeed.Common.Extensions;
using GameFeed.Domain.ApiEntities;
using GameFeed.Domain.Models;
using Newtonsoft.Json;
using RestSharp;

namespace GameFeed.Domain.ApiRepositories {

    public interface IApiClient {
        T Get<T>(string endpoint) where T : new();
        IEnumerable<T> GetMultiple<T>(string endpoint);
        ScrollResponse Scroll<T>(string endpoint);
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
        /// Returns a scrollable list of the requested objects of type 'T' from the API along with
        /// the scroll-link and page count. https://igdb.github.io/api/references/pagination/
        /// </summary>
        /// <typeparam name="T">The return type</typeparam>
        /// <param name="endpoint">The endpoint after the base URL</param>
        /// <returns>A ScrollResponse containing the list of objects of type 'T' and further scroll arguments</returns>
        public ScrollResponse Scroll<T>(string endpoint) {
            IRestResponse response = ExecuteRequest($"{endpoint}&scroll=1");

            if (response.StatusCode == HttpStatusCode.BadRequest)
                throw new GameFeedException("Expired or invalid scroll context.");

            try {
                return new ScrollResponse {
                    Scrollables = (IEnumerable<IScrollable>)JsonConvert.DeserializeObject<List<T>>(response.Content),
                    ScrollUrl = (string)response.Headers.FirstOrDefault(h => h.Name == "X-Next-Page")?.Value,
                    PageCount = ((string)response.Headers.FirstOrDefault(h => h.Name == "X-Count")?.Value).ParseNullableInt()
                };
            } catch (InvalidCastException icex) {
                throw new GameFeedException("Given scroll type is not of type IScrollable! If the type is correct, make it inherit IScrollable.", icex);
            }
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
