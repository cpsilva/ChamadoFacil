using ChamadoFacil.ApplicationService.Contracts;
using ChamadoFacil.BusinessLogic.Chamado;
using ChamadoFacil.Models.Chamado;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Routing;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace ChamadoFacil.ApplicationService.Endpoints
{
    public static class ChamadosEndpoints
    {
        public static void MapChamados(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/chamados").WithTags("Chamados").RequireAuthorization();

            group.MapGet("/{id:int}", GetById)
                .WithName("GetChamadoById")
                .WithSummary("Get a chamado by ID")
                .Produces<ChamadoResponse>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound);

            group.MapGet("/", GetAll)
                .WithName("GetChamados")
                .WithSummary("Get all chamados")
                .Produces<IEnumerable<ChamadoResponse>>(StatusCodes.Status200OK);

            group.MapPost("/", Create)
                .WithName("CreateChamado")
                .WithSummary("Create a new chamado")
                .Produces<ChamadoResponse>(StatusCodes.Status201Created);

            group.MapPut("/{id:int}", Update)
                .WithName("UpdateChamado")
                .WithSummary("Update an existing chamado")
                .Produces<ChamadoResponse>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound);

            group.MapDelete("/{id:int}", Delete)
                .WithName("DeleteChamado")
                .WithSummary("Delete a chamado")
                .Produces(StatusCodes.Status204NoContent);
        }

        private static async Task<Results<Ok<ChamadoResponse>, NotFound>> GetById(
            int id, IChamadoBll chamadoBll, CancellationToken cancellationToken)
        {
            var chamado = await chamadoBll.SelecionarAsync(id, cancellationToken);
            return chamado is null ? TypedResults.NotFound() : TypedResults.Ok(ToResponse(chamado));
        }

        private static async Task<Ok<IEnumerable<ChamadoResponse>>> GetAll(
            IChamadoBll chamadoBll, CancellationToken cancellationToken)
        {
            var chamados = await chamadoBll.ListarAsync(cancellationToken);
            return TypedResults.Ok(chamados.Select(ToResponse));
        }

        private static async Task<Created<ChamadoResponse>> Create(
            CreateChamadoRequest request, ClaimsPrincipal user, IChamadoBll chamadoBll, CancellationToken cancellationToken)
        {
            var chamadoModel = new ChamadoModel
            {
                Titulo = request.Titulo,
                Mensagem = request.Mensagem,
                Urgencia = request.Urgencia,
                CategoriaId = request.CategoriaId,
                UsuarioId = GetUserId(user)
            };

            await chamadoBll.SalvarAsync(chamadoModel, cancellationToken);

            return TypedResults.Created($"/api/chamados/{chamadoModel.Id}", ToResponse(chamadoModel));
        }

        private static async Task<Results<Ok<ChamadoResponse>, NotFound>> Update(
            int id, UpdateChamadoRequest request, IChamadoBll chamadoBll, CancellationToken cancellationToken)
        {
            var existing = await chamadoBll.SelecionarAsync(id, cancellationToken);
            if (existing is null)
            {
                return TypedResults.NotFound();
            }

            existing.Titulo = request.Titulo;
            existing.Mensagem = request.Mensagem;
            existing.Urgencia = request.Urgencia;
            existing.CategoriaId = request.CategoriaId;

            await chamadoBll.SalvarAsync(existing, cancellationToken);

            return TypedResults.Ok(ToResponse(existing));
        }

        private static async Task<NoContent> Delete(
            int id, IChamadoBll chamadoBll, CancellationToken cancellationToken)
        {
            await chamadoBll.ApagarAsync(id, cancellationToken);
            return TypedResults.NoContent();
        }

        private static ChamadoResponse ToResponse(ChamadoModel model) =>
            new(model.Id, model.Titulo, model.Mensagem, model.Urgencia, model.CategoriaId, model.UsuarioId);

        private static int GetUserId(ClaimsPrincipal user) =>
            int.Parse(user.FindFirstValue(JwtRegisteredClaimNames.Sub));
    }
}
