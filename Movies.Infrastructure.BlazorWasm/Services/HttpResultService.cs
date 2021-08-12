using Microsoft.AspNetCore.Components.WebAssembly.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Movies.BusinessLogic.Results;
using Movies.Infrastructure.BlazorWasm.Options;
using Movies.Infrastructure.BlazorWasm.Services.Interfaces;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Movies.Infrastructure.BlazorWasm.Services
{
    public class HttpResultService : IHttpResultService
    {        
        private readonly HttpClient httpClient;
        private readonly IConfiguration configuration;
        //private readonly HttpOptions httpOptions;
        private readonly string baseUri;

        public HttpResultService(HttpClient httpClient, IConfiguration configuration)
        {
            this.httpClient = httpClient;
            this.configuration = configuration;

            //httpOptions = this.configuration.GetValue<HttpOptions>(HttpOptions.Position);
            baseUri = configuration["ServerEndpoint"];
        }

        public async Task<Result<T>> GetAsync<T>(string endpoint, bool withCredentials = false)
        {
            var result = new Result<T>();
            await ResultHandler.TryExecuteAsync(result, GetAsync<T>(endpoint, withCredentials, result));
            return result;
        }

        public async Task<Result<T>> GetAsync<T>(string endpoint, bool withCredentials, Result<T> result)
        {
            var response = new HttpResponseMessage();
            if (withCredentials)
            {
                var message = new HttpRequestMessage
                {
                    Method = new HttpMethod("Get"),
                    RequestUri = new Uri(baseUri + endpoint)
                };

                response = await httpClient.SendAsync(message);
            }

            else
            {
                response = await httpClient.GetAsync(endpoint);
            }

            await DeserializeAsync(result, response);

            return result;
        }

        public async Task<Result<T>> PostAsync<T>(string endpoint, T request)
        {
            var result = new Result<T>();
            await ResultHandler.TryExecuteAsync(result, PostAsync(endpoint, request, result));
            return result;
        }       

        public async Task<Result<T>> PostAsync<T>(string endpoint, T request, Result<T> result)
        {
            return await SendRequestAsync(endpoint, request, "POST", result);
        }

        public async Task<Result<T1>> SendRequestAsync<T, T1>(string endpoint, T request, string method, Result<T1> result)
        {
            var content = JsonContent.Create<T>(request);

            var requestMessage = new HttpRequestMessage()
            {
                Method = new HttpMethod(method),
                RequestUri = new Uri($"{baseUri}/{endpoint}"),
                Content = content
            };

            requestMessage.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);

            var response = await httpClient.SendAsync(requestMessage);

            await DeserializeAsync(result, response);

            return result;
        }

        public async Task<Result> SendRequestAsync(string endpoint, string method, Result result)
        {
            var requestMessage = new HttpRequestMessage()
            {
                Method = new HttpMethod(method),
                RequestUri = new Uri($"{baseUri}/{endpoint}")
            };

            requestMessage.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);

            var response = await httpClient.SendAsync(requestMessage);

            await DeserializeAsync(result, response);

            return result;
        }

        public async Task<Result<T>> PutAsync<T>(string endpoint, T request)
        {
            var result = new Result<T>();
            await ResultHandler.TryExecuteAsync(result, PutAsync(endpoint, request, result));
            return result;
        }

        public async Task<Result<T>> PutAsync<T>(string endpoint, T request, Result<T> result)
        {
            return await SendRequestAsync(endpoint, request, "PUT", result);
        }

        public async Task<Result> DeleteAsync(string endpoint)
        {
            var result = new Result();
            await ResultHandler.TryExecuteAsync(result, PutAsync(endpoint, result));
            return result;
        }

        public async Task<Result> DeleteAsync(string endpoint, Result result)
        {
            return await SendRequestAsync(endpoint, "Delete", result);
        }

        public async Task DeserializeAsync<T>(Result<T> result, HttpResponseMessage response)
        {
            var responseContent = await response.Content.ReadAsStringAsync();
            var deserialized = JsonConvert.DeserializeObject<Result<T>>(responseContent);

            result.Title = deserialized.Title;

            foreach (var item in deserialized.Errors)
            {
                result.Errors.Add(item.Key, item.Value);
            }

            result.Value = deserialized.Value;
        }

        public async Task DeserializeAsync(Result result, HttpResponseMessage response)
        {
            var responseContent = await response.Content.ReadAsStringAsync();
            var deserialized = JsonConvert.DeserializeObject<Result>(responseContent);

            result.Title = deserialized.Title;

            foreach (var item in deserialized.Errors)
            {
                result.Errors.Add(item.Key, item.Value);
            }
        }
    }
}
