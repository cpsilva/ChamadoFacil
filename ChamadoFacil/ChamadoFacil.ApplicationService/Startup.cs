using ChamadoFacil.ApplicationService.Extensions;
using ChamadoFacil.DataAccess.Database.Context;
using ChamadoFacil.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ChamadoFacil.ApplicationService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDependencyInjectionCustom(Configuration);
            services.AddAuthenticationCustom();
            services.AddCors();
            services.AddMvcCustom();
            Container.RegisterServices(services);
            Container.AddDbContext<DatabaseContext>("Data Source=PE00Q1ME\\SQLEXPRESS;Initial Catalog=ChamadoFacil;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseExceptionCustom(env);
            app.UseAuthentication();
            app.UseCorsCustom();
            app.UseMvcWithDefaultRoute();
        }
    }
}