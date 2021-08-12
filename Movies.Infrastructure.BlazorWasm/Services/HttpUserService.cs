using Movies.BusinessLogic.Results;
using Movies.BusinessLogic.Services.Interfaces;
using Movies.Domain.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using Movies.Infrastructure.BlazorWasm.Services.Interfaces;

namespace Movies.Infrastructure.BlazorWasm.Services
{
    public class HttpUserService : IUserService
    {
        private readonly IHttpResultService httpResultService;

        public HttpUserService(IHttpResultService httpResultService)
        {
            this.httpResultService = httpResultService;
        }

        public async Task<Result> DeleteAccountAsync(int id)
        {
            var result = new Result();
            await ResultHandler.TryExecuteAsync(result, DeleteAccountAsync(id, result));
            return result;
        }

        protected async Task<Result> DeleteAccountAsync(int id, Result result)
        {
            await httpResultService.DeleteAsync($"Users/account/{id}", result);

            return result;
        }

        public async Task<Result<User>> GetUserAccountAsync(int id)
        {
            var result = new Result<User>();
            await ResultHandler.TryExecuteAsync(result, GetUserAccountAsync(id, result));
            return result;
        }

        protected async Task<Result<User>> GetUserAccountAsync(int id, Result<User> result)
        {
            await httpResultService.GetAsync<User>($"Users/account/{id}", true, result);

            return result;
        }

        public Task<IEnumerable<UserRoles>> GetUserRolesAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<User>> LoginAsync(User request)
        {
            var result = new Result<User>();
            await ResultHandler.TryExecuteAsync(result, LoginAsync(request, result));
            return result;
        }

        protected async Task<Result<User>> LoginAsync(User user, Result<User> result)
        {
            await httpResultService.PostAsync<User>("Users/account/login", user, result);
            
            return result;
        }

        public Task<Result<User>> RegisterAsync(User userRequest)
        {
            throw new NotImplementedException();
        }

        public Task<Result<User>> UpdateAccountAsync(User request)
        {
            throw new NotImplementedException();
        }
    }
}
