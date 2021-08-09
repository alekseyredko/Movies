using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Movies.Domain.Models;
using Movies.Infrastructure.Validators.User;

namespace Movies.Infrastructure.Extensions
{
    public static class ValidationExtensions
    {
        public static void AddValidationExtensions(this IMvcBuilder mvcBuilder)
        {
            mvcBuilder.AddFluentValidation();
        }

        public static void RegisterValidatorsAsServices(this IServiceCollection collection)
        {
            collection.AddTransient<IValidator<User>, UserValidator>();
        }
    }
}
