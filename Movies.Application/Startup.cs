using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Movies.Infrastructure.Authentication;
using Movies.Infrastructure.Extensions;
using Movies.Infrastructure.Services;
using Movies.Infrastructure.Services.Extensions;
using System;
using System.IO;
using System.Reflection;

namespace Movies.Application
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();

            services.AddControllers()
                .AddNewtonsoftJson(x =>
                    x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
                .AddFluentValidation();


            services.RegisterValidatorsAsServices();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Movies.Infrastructure", 
                    Version = "v1",
                    Description = "API to access movie catalog"
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            services.AddDataAccessServices();
           
            //services.AddFilters();

            AddAuthentication(services);

            services.AddTransient<IAuthorizationMiddlewareResultHandler, CustomAuthorizationMiddlewareHandler>();

            services.AddAutomapperAndProfile();
        }

        private void AddAuthentication(IServiceCollection services)
        {
            var config = Configuration.GetSection("Auth");
            services.Configure<AuthConfiguration>(config);
            services.AddAuthentication(config.Get<AuthConfiguration>());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Movies.Infrastructure v1"));
            }

            //app.UseHttpsRedirection();            

            app.UseRouting();

            app.UseCors(options =>
            {
                options.WithOrigins("http://localhost:30261")
                       .AllowCredentials()
                       .AllowAnyMethod()
                       .SetIsOriginAllowed((host) => true)
                       .AllowAnyHeader();
            });

            app.Use(async (context, next) =>
            {
                var token = context.Request.Cookies["Token"];
                if (!string.IsNullOrEmpty(token) && !context.Request.Headers.ContainsKey("Authorization"))
                {
                    context.Request.Headers.Add("Authorization", "Bearer " + token);
                }

                await next();
            });            

            app.UseAuthentication();
            app.UseAuthorization();            

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
