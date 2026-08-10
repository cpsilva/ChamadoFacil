using ChamadoFacil.ApplicationService.Contracts;
using ChamadoFacil.BusinessLogic.Authentication;
using ChamadoFacil.Models.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Routing;
using System.Threading;
using System.Threading.Tasks;

namespace ChamadoFacil.ApplicationService.Endpoints
{
    public static class AuthEndpoints
    {
        public static void MapAuth(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/auth").WithTags("Auth").AllowAnonymous();

            group.MapPost("/login", Login)
                .WithName("Login")
                .WithSummary("Log in with email and password")
                .Produces<TokenResponse>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status401Unauthorized);

            group.MapPost("/refresh", Refresh)
                .WithName("RefreshToken")
                .WithSummary("Exchange a refresh token for a new token pair")
                .Produces<TokenResponse>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status401Unauthorized);

            group.MapPost("/logout", Logout)
                .WithName("Logout")
                .WithSummary("Revoke a refresh token")
                .Produces(StatusCodes.Status204NoContent);
        }

        private static async Task<Results<Ok<TokenResponse>, UnauthorizedHttpResult>> Login(
            LoginRequest request, IAuthenticationBll authenticationBll, CancellationToken cancellationToken)
        {
            var authentication = new AuthenticationModel { Login = request.Login, Password = request.Password };
            var usuario = await authenticationBll.AuthenticateAsync(authentication, cancellationToken);

            if (usuario is null)
            {
                return TypedResults.Unauthorized();
            }

            var tokens = await authenticationBll.IssueTokensAsync(usuario, cancellationToken);
            return TypedResults.Ok(new TokenResponse(tokens.AccessToken, tokens.RefreshToken));
        }

        private static async Task<Results<Ok<TokenResponse>, UnauthorizedHttpResult>> Refresh(
            RefreshRequest request, IAuthenticationBll authenticationBll, CancellationToken cancellationToken)
        {
            var tokens = await authenticationBll.RefreshAsync(request.RefreshToken, cancellationToken);

            if (tokens is null)
            {
                return TypedResults.Unauthorized();
            }

            return TypedResults.Ok(new TokenResponse(tokens.AccessToken, tokens.RefreshToken));
        }

        private static async Task<NoContent> Logout(
            LogoutRequest request, IAuthenticationBll authenticationBll, CancellationToken cancellationToken)
        {
            await authenticationBll.LogoutAsync(request.RefreshToken, cancellationToken);
            return TypedResults.NoContent();
        }
    }
}
