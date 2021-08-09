using Microsoft.Extensions.DependencyInjection;
using Movies.BusinessLogic.Database.Services;
using Movies.BusinessLogic.Database.Services.Decorators;
using Movies.BusinessLogic.Services.Interfaces;
using Movies.DataAccess;
using Movies.DataAccess.Interfaces;
using Movies.Infrastructure.Services;
using Movies.Infrastructure.Services.Interfaces;

namespace Movies.Infrastructure.Services.Extensions
{
    public static class DataAccessExtensions
    {
        public static void AddDataAccessServices(this IServiceCollection services)
        {
            services.AddDbContext<MoviesDBContext>(optionsLifetime: ServiceLifetime.Transient);
            AddServices(services);
        }        
       
        private static void AddServices(IServiceCollection services)
        {            
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddTransient<IActorsService, ActorsService>();
            services.AddTransient<IMovieService, MovieService>();
            services.AddTransient<IPersonService, PersonService>();
            services.AddTransient<IProducerService, ProducerService>();
            services.AddTransient<IReviewService, ReviewService>();            
            services.AddTransient<IRefreshTokenService, RefreshTokenService>();
            services.AddTransient<IUserService, UserService>();

            //services.AddTransient<IUserService, TokenUserService>(factory =>
            //{
            //    var unitOfWork = factory.GetRequiredService<IUnitOfWork>();
            //    var logger = factory.GetRequiredService<IValidator<User>>();
            //    var config = factory.GetRequiredService<IOptions<AuthConfiguration>>();
            //    var validator = factory.GetRequiredService<IValidator<User>>();
            //    var service = new UserService(unitOfWork, validator);
            //    var tokenService = factory.GetRequiredService<IRefreshTokenService>();

            //    return new TokenUserService(unitOfWork, config, service, tokenService);
            //});
        }
    }
}
