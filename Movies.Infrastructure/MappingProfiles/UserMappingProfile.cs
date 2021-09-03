using AutoMapper;
using Movies.Infrastructure.Models;
using Movies.Infrastructure.Models.User;
using Movies.BusinessLogic.Results;
using Movies.Domain.Models;

namespace Movies.Infrastructure.MappingProfiles
{
    public class UserMappingProfile: Profile
    {
        public UserMappingProfile()
        {
            CreateMap<LoginUserRequest, User>();
            CreateMap<User, LoginUserResponse>();
            CreateMap<Result<User>, Result<LoginUserResponse>>();

            CreateMap<RegisterUserRequest, User>();
            CreateMap<User, RegisterUserResponse>();
            CreateMap<Result<User>, Result<RegisterUserResponse>>();


            CreateMap<UpdateUserRequest, User>();
            CreateMap<User, UpdateUserResponse>();
            CreateMap<Result<User>, Result<UpdateUserResponse>>();

            CreateMap<User, GetUserResponse>();
            CreateMap<Result<User>, Result<GetUserResponse>>();

            CreateMap<Result, Result<TokenResponse>>();
        }
    }
}
