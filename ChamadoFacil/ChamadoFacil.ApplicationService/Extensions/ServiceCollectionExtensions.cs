using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ChamadoFacil.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using ChamadoFacil.CrossCutting.Security.JsonWebToken;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.Authorization;

namespace ChamadoFacil.ApplicationService.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddDependencyInjectionCustom(this IServiceCollection services, IConfiguration configuration)
        {
            Container.RegisterServices(services);
        }

        public static void AddAuthenticationCustom(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();

            services.AddOptions<JwtBearerOptions>(JwtBearerDefaults.AuthenticationScheme)
                .Configure<IJWT>((jwtBearer, jwt) =>
                {
                    jwtBearer.RequireHttpsMetadata = false;
                    jwtBearer.SaveToken = true;
                    jwtBearer.TokenValidationParameters = jwt.GetTokenValidationParameters();
                });
        }

        public static void AddMvcCustom(this IServiceCollection services)
        {
            void Mvc(MvcOptions mvc)
            {
                mvc.EnableEndpointRouting = false;
                var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                mvc.Filters.Add(new AuthorizeFilter(policy));
            }

            void Json(MvcNewtonsoftJsonOptions json)
            {
                json.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            }

            services.AddMvc(Mvc).AddNewtonsoftJson(Json);
        }
    }
}