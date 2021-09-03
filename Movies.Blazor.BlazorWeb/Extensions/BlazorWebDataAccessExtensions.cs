using Microsoft.Extensions.DependencyInjection;
using Movies.BusinessLogic.Database.Services.Decorators;
using Movies.BusinessLogic.Services.Interfaces;
using Movies.DataAccess;

namespace Movies.Blazor.BlazorWeb.Extensions
{
    public static class BlazorWebDataAccessExtensions
    {
        public static void AddDataAccessServicesForBlazorWeb(this IServiceCollection services)
        {
            services.AddDbContextFactory<MoviesDBContext>();
            //services.AddTransient<IUnitOfWork, UnitOfWork>();

            //services.AddTransient<IActorsService, ActorsService>();
            services.AddTransient<IMovieService, MovieServiceDecorator>();
            //services.AddTransient<IPersonService, PersonService>();
            services.AddTransient<IProducerService, ProducerServiceDecorator>();
            services.AddTransient<IReviewService, ReviewServiceDecorator>();
            services.AddTransient<IUserService, UserSeviceDecorator>();
        }
    }
}
