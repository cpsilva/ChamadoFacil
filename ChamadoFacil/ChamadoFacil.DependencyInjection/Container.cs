using ChamadoFacil.BusinessLogic.Authentication;
using ChamadoFacil.BusinessLogic.Chamado;
using ChamadoFacil.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace ChamadoFacil.DependencyInjection
{
    public static class Container
    {
        private static IServiceProvider _serviceProvider;

        private static IServiceCollection _services;

        public static T GetService<T>()
        {
            if (_services == null)
            {
                _services = RegisterServices();
            }

            if (_serviceProvider == null)
            {
                _serviceProvider = _services.BuildServiceProvider();
            }

            return _serviceProvider.GetService<T>();
        }

        public static IServiceCollection RegisterServices(IServiceCollection services = null)
        {
            _services = services ?? new ServiceCollection();

            _services.AddScoped<IAuthenticationBll, AuthenticationBll>();
            _services.AddScoped<IChamadoBll, ChamadoBll>();
            _services.AddScoped<IUnitOfWork, UnitOfWork>();
            _services.AddScoped<IQueryStack, QueryStack>();
            _services.AddScoped<ICommandStack, CommandStack>();

            return _services;
        }

        public static void AddDbContext<T>(string connectionString) where T : DbContext
        {
            _services.AddDbContext<T>(options => options.UseSqlServer(connectionString));
            var context = GetService<T>();
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            context.Database.Migrate();
        }

        public static void AddDbContextInMemoryDatabase<T>() where T : DbContext
        {
            _services.AddDbContext<T>(options => options.UseInMemoryDatabase(typeof(T).Name));
            GetService<T>().Database.EnsureCreated();
        }
    }
}