using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ChamadoFacil.DependencyInjection;

namespace ChamadoFacil.ApplicationService.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddDependencyInjectionCustom(this IServiceCollection services, IConfiguration configuration)
        {
            Container.RegisterServices(services);
        }
    }
}