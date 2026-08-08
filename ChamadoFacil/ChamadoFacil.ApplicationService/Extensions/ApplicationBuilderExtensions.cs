using ChamadoFacil.ApplicationService.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace ChamadoFacil.ApplicationService.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseCorsCustom(this IApplicationBuilder application)
        {
            void CorsPolicy(CorsPolicyBuilder corsPolicy)
            {
                corsPolicy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
            }

            application.UseCors(CorsPolicy);
        }

        public static void UseExceptionCustom(this IApplicationBuilder application, IWebHostEnvironment environment)
        {
            if (environment.IsDevelopment())
            {
                application.UseDeveloperExceptionPage();
            }

            application.UseExceptionMiddleware();
        }

        public static void UseExceptionMiddleware(this IApplicationBuilder application)
        {
            application.UseMiddleware<ExceptionMiddleware>();
        }
    }
}