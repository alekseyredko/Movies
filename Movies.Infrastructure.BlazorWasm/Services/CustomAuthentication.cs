using AutoMapper;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using Movies.BusinessLogic.Results;
using Movies.BusinessLogic.Services.Interfaces;
using Movies.Domain.Models;
using Movies.Infrastructure.BlazorWasm.Services.Interfaces;
using Movies.Infrastructure.Models;
using Movies.Infrastructure.Models.Producer;
using Movies.Infrastructure.Models.Reviewer;
using Movies.Infrastructure.Models.User;
using Movies.Infrastructure.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Infrastructure.BlazorWasm.Services
{
    public class CustomAuthentication : ICustomAuthentication
    {
        private readonly AuthenticationStateProvider authenticationHandlerProvider;
        private readonly IMapper mapper;
        private readonly IUserService userService;
        private readonly IJSRuntime jSRuntime;
        private readonly IHttpResultService httpResultService;

        public CustomAuthentication(AuthenticationStateProvider authenticationHandlerProvider,
                                    IMapper mapper,
                                    IJSRuntime jSRuntime,
                                    IUserService userService, 
                                    IHttpResultService httpResultService)
        {
            this.authenticationHandlerProvider = authenticationHandlerProvider;
            this.mapper = mapper;
            this.jSRuntime = jSRuntime;
            this.userService = userService;
            this.httpResultService = httpResultService;
        }

        public async Task<Result<GetUserResponse>> GetCurrentUserDataAsync()
        {
            var user = new Result<User>
            {
                ResultType = ResultType.Unauthorized
            };

            var state = await authenticationHandlerProvider.GetAuthenticationStateAsync();            

            if (state.User.Identity.IsAuthenticated)
            {               
                var claim = state.User.Claims.FirstOrDefault(x => x.Type == "sub");

                if (claim != null)
                {
                    if (int.TryParse(claim.Value, out int id))
                    {
                        user = await userService.GetUserAccountAsync(id);
                    }
                }
                else
                {
                    user.ResultType = ResultType.Unauthorized;
                }
            }

            return mapper.Map<Result<User>, Result<GetUserResponse>>(user);
        }

        public async Task<Result> LogoutAsync()
        {
            var result = new Result();
            await ResultHandler.TryExecuteAsync(result, LogoutAsync(result));

            return result;
        }

        protected async Task<Result> LogoutAsync(Result result)
        {
            await httpResultService.SendRequestAsync("Users/account/logout", "POST", result);
            return result;
        }

        public async Task<Result<LoginUserResponse>> TryLoginAsync(LoginUserRequest request)
        {
            var mapped = mapper.Map<User>(request);
            var result = await userService.LoginAsync(mapped);
            var mappedResult = mapper.Map<Result<LoginUserResponse>>(result);

            if (mappedResult.ResultType == ResultType.Ok)
            {
                (authenticationHandlerProvider as UserAuthStateProvider).Notify();
            }

            return mappedResult;
        }

        public Task<Result<ProducerResponse>> TryRegisterAsProducerAsync(ProducerRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<Result<RegisterReviewerResponse>> TryRegisterAsReviewerAsync(RegisterReviewerRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<Result<RegisterUserResponse>> TryRegisterAsync(RegisterUserRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
