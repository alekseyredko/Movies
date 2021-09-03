using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Movies.Infrastructure.BlazorWasm.Extensions;
using Movies.Infrastructure.BlazorWasm.Services;
using Movies.Infrastructure.Extensions;
using Movies.Infrastructure.Services.Interfaces;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Movies.Infrastructure.BlazorWasm.Options;
using Movies.Infrastructure.BlazorWasm.Services.Interfaces;

namespace Movies.Blazor.BlazorWasm
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");            

            builder.Services.AddHttpClient("WebAPI", client =>
            {
                client.BaseAddress = new Uri("http://localhost:5000/api/");                
                //client.DefaultRequestHeaders.Add("Access-Control-Allow-Credentials", "true");                
            });

            builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>()
                .CreateClient("WebAPI"));

            builder.Services.AddScoped<IHttpResultService, HttpResultService>();
            builder.Services.AddDataAccessServices();

            builder.Services.AddScoped<AuthenticationStateProvider, UserAuthStateProvider>();
            builder.Services.AddScoped<ICustomAuthentication, CustomAuthentication>();

            builder.Services.AddOptions();
            builder.Services.AddAuthorizationCore();

            builder.Services.AddAutomapperAndProfile();                                    

            await builder.Build().RunAsync();
        }
    }
}
