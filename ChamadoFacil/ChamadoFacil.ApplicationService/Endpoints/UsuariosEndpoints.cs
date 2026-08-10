using ChamadoFacil.ApplicationService.Contracts;
using ChamadoFacil.BusinessLogic.Usuario;
using ChamadoFacil.Models.User;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Routing;
using System.Threading;
using System.Threading.Tasks;

namespace ChamadoFacil.ApplicationService.Endpoints
{
    public static class UsuariosEndpoints
    {
        public static void MapUsuarios(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/usuarios").WithTags("Usuarios");

            group.MapPost("/", Register)
                .AllowAnonymous()
                .WithName("RegisterUsuario")
                .WithSummary("Register a new usuario")
                .Produces(StatusCodes.Status201Created);
        }

        private static async Task<Created> Register(
            RegisterUsuarioRequest request, IUsuarioBll usuarioBll, CancellationToken cancellationToken)
        {
            var usuario = new UsuarioModel
            {
                Nome = request.Nome,
                Email = request.Email,
                Senha = request.Senha
            };

            await usuarioBll.CadastrarAsync(usuario, cancellationToken);

            return TypedResults.Created($"/api/usuarios/{usuario.Id}");
        }
    }
}
