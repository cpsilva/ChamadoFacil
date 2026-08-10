using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;

namespace ChamadoFacil.ApplicationService.Test
{
    public class ApiTestFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureAppConfiguration((_, config) =>
            {
                config.AddInMemoryCollection(new Dictionary<string, string>
                {
                    ["ConnectionStrings:DatabaseContext"] = "Server=(localdb)\\mssqllocaldb;Database=ChamadoFacilApiTests;Trusted_Connection=True;Encrypt=False",
                    ["JwtSettings:SecurityKey"] = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                    ["JwtSettings:Audience"] = "ChamadoFacil.Client.Tests",
                    ["JwtSettings:Issuer"] = "ChamadoFacil.Api.Tests"
                });
            });
        }
    }
}
