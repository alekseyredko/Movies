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
        private readonly string baseUri;

        public HttpResultService(HttpClient httpClient, IConfiguration configuration)
        {
            this.httpClient = httpClient;
            this.configuration = configuration;

            
            baseUri = configuration["ServerEndpoint"];
        }

        public async Task<Result<T1>> SendRequestAsync<T1>(string endpoint, string method, Result<T1> result)
        {
            var response = await SendAsync(endpoint, method);

            await DeserializeAsync(result, response);

            return result;
        }        

        public async Task<Result<T1>> SendRequestAsync<T, T1>(string endpoint, T request, string method, Result<T1> result)
        {
            var response = await SendAsync(endpoint, request, method);

            await DeserializeAsync(result, response);

            return result;
        }       

        public async Task<Result> SendRequestAsync(string endpoint, string method, Result result)
        {
            var response = await SendAsync(endpoint, method);

            await DeserializeAsync(result, response);

            return result;
        }

        private async Task<HttpResponseMessage> SendAsync(string endpoint, string method, JsonContent content = null)
        {
            var requestMessage = new HttpRequestMessage()
            {
                Method = new HttpMethod(method),
                RequestUri = new Uri($"{baseUri}/{endpoint}"),
                Content = content
            };

            requestMessage.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);

            var response = await httpClient.SendAsync(requestMessage);
            return response;
        }

        private async Task<HttpResponseMessage> SendAsync<T>(string endpoint, T request, string method)
        {
            var content = JsonContent.Create<T>(request);

            return await SendAsync(endpoint, method, content);
        }

        public async Task DeserializeAsync<T>(Result<T> result, HttpResponseMessage response)
        {
            var responseContent = await response.Content.ReadAsStringAsync();

            if (responseContent == null)
            {
                result.ResultType = ResultType.Unexpected;
                return;
            }           

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

            if (responseContent == null)
            {
                result.ResultType = ResultType.Unexpected;
                return;
            }
            
            var deserialized = JsonConvert.DeserializeObject<Result>(responseContent);

            result.Title = deserialized.Title;

            foreach (var item in deserialized.Errors)
            {
                result.Errors.Add(item.Key, item.Value);
            }
        }

        public async Task<Result> TryRefreshTokenAsync()
        {
            var result = new Result();

            var getTokenResult = await SendAsync("Users/account/refresh-token", "POST");

            if (getTokenResult.IsSuccessStatusCode)
            {
                result.ResultType = ResultType.Ok;
            }

            else
            {
                result.ResultType = ResultType.Forbidden;
            }

            return result;
        }
    }
}
