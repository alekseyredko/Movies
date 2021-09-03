using System.Threading.Tasks;
using Movies.BusinessLogic.Results;
using Movies.Infrastructure.Models;
using Movies.Infrastructure.Models.Producer;
using Movies.Infrastructure.Models.Reviewer;
using Movies.Infrastructure.Models.User;

namespace Movies.Infrastructure.Services.Interfaces
{
    public interface ICustomAuthentication
    {
        Task<Result<LoginUserResponse>> TryLoginAsync(LoginUserRequest request);
        Task<Result<RegisterUserResponse>> TryRegisterAsync(RegisterUserRequest request);        
        Task<Result> LogoutAsync();
        Task<Result<ProducerResponse>> TryRegisterAsProducerAsync(ProducerRequest request);
        Task<Result<GetUserResponse>> GetCurrentUserDataAsync();
        Task<Result<RegisterReviewerResponse>> TryRegisterAsReviewerAsync(RegisterReviewerRequest request);
    }
}
