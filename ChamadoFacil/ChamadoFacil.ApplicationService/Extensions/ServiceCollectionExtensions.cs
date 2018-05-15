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

        public static void AddAuthenticationCustom(this IServiceCollection services)
        {
            void JwtBearer(JwtBearerOptions jwtBearer)
            {
                jwtBearer.RequireHttpsMetadata = false;
                jwtBearer.SaveToken = true;
                jwtBearer.TokenValidationParameters = new Jwt().GetTokenValidationParameters();
            }

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(JwtBearer);
        }

        public static void AddMvcCustom(this IServiceCollection services)
        {
            void Mvc(MvcOptions mvc)
            {
                var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                mvc.Filters.Add(new AuthorizeFilter(policy));
            }

            void Json(MvcJsonOptions json)
            {
                json.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            }

            services.AddMvc(Mvc).AddJsonOptions(Json);
        }
    }
}