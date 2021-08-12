using Movies.BusinessLogic.Results;
using System.Net.Http;
using System.Threading.Tasks;

namespace Movies.Infrastructure.BlazorWasm.Services.Interfaces
{
    public interface IHttpResultService
    {
        Task<Result> DeleteAsync(string endpoint);
        Task<Result> DeleteAsync(string endpoint, Result result);
        Task DeserializeAsync(Result result, HttpResponseMessage response);
        Task DeserializeAsync<T>(Result<T> result, HttpResponseMessage response);
        Task<Result<T>> GetAsync<T>(string endpoint, bool withCredentials = false);
        Task<Result<T>> GetAsync<T>(string endpoint, bool withCredentials, Result<T> result);
        Task<Result<T>> PostAsync<T>(string endpoint, T request);
        Task<Result<T>> PostAsync<T>(string endpoint, T request, Result<T> result);
        Task<Result<T>> PutAsync<T>(string endpoint, T request);
        Task<Result<T>> PutAsync<T>(string endpoint, T request, Result<T> result);
        Task<Result> SendRequestAsync(string endpoint, string method, Result result);
        Task<Result<T1>> SendRequestAsync<T, T1>(string endpoint, T request, string method, Result<T1> result);
    }
}