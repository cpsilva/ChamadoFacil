using ChamadoFacil.ApplicationService.Endpoints;
using ChamadoFacil.ApplicationService.Middleware;
using ChamadoFacil.CrossCutting.Security.JsonWebToken;
using ChamadoFacil.DataAccess.Database.Context;
using ChamadoFacil.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));

Container.RegisterServices(builder.Services);
Container.AddDbContext<DatabaseContext>(builder.Configuration.GetConnectionString(nameof(DatabaseContext)));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();
builder.Services.AddOptions<JwtBearerOptions>(JwtBearerDefaults.AuthenticationScheme)
    .Configure<IJWT>((jwtBearer, jwt) =>
    {
        jwtBearer.RequireHttpsMetadata = false;
        jwtBearer.SaveToken = true;
        jwtBearer.MapInboundClaims = false;
        jwtBearer.TokenValidationParameters = jwt.GetTokenValidationParameters();
    });
builder.Services.AddAuthorization();

builder.Services.AddCors(options =>
    options.AddDefaultPolicy(policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

builder.Services.AddExceptionHandler<ApiExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddOpenApi();
builder.Services.AddValidation();

var app = builder.Build();

app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

app.MapChamados();
app.MapCategorias();
app.MapUsuarios();
app.MapAuth();

app.Run();

public partial class Program
{
}
