using Microsoft.Extensions.DependencyInjection;
using Movies.BusinessLogic.Services.Interfaces;
using Movies.Infrastructure.BlazorWasm.Services;
using Movies.Infrastructure.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Infrastructure.BlazorWasm.Extensions
{
    public static class BlazorWasmDataExtensions
    {
        public static void AddDataAccessServices(this IServiceCollection serviceCollection)
        {                       
            serviceCollection.AddScoped<IUserService, HttpUserService>();
            serviceCollection.AddScoped<IMovieService, HttpMovieService>();
        }
    }
}
