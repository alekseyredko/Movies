using Movies.BusinessLogic.Results;
using System.Net.Http;
using System.Threading.Tasks;

namespace Movies.Infrastructure.BlazorWasm.Services.Interfaces
{
    public interface IHttpResultService
    {        
        //Task DeserializeAsync(Result result, HttpResponseMessage response);
        //Task DeserializeAsync<T>(Result<T> result, HttpResponseMessage response);       
        Task<Result> SendRequestAsync(string endpoint, string method, Result result);
        Task<Result<T>> SendRequestAsync<T>(string endpoint, string method, Result<T> result);
        Task<Result<T1>> SendRequestAsync<T, T1>(string endpoint, T request, string method, Result<T1> result);
        Task<Result> TryRefreshTokenAsync();
    }
}