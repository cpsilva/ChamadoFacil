using ChamadoFacil.ApplicationService.Contracts;
using ChamadoFacil.BusinessLogic.Categoria;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Routing;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ChamadoFacil.ApplicationService.Endpoints
{
    public static class CategoriasEndpoints
    {
        public static void MapCategorias(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/categorias").WithTags("Categorias").RequireAuthorization();

            group.MapGet("/", GetAll)
                .WithName("GetCategorias")
                .WithSummary("Get all categorias for a dropdown selector")
                .Produces<IEnumerable<CategoriaOptionResponse>>(StatusCodes.Status200OK);
        }

        private static async Task<Ok<IEnumerable<CategoriaOptionResponse>>> GetAll(
            ICategoriaBll categoriaBll, CancellationToken cancellationToken)
        {
            var categorias = await categoriaBll.ListarAsync(cancellationToken);
            var response = categorias.OrderBy(c => c.Descricao).Select(c => new CategoriaOptionResponse(c.Id, c.Descricao));
            return TypedResults.Ok(response);
        }
    }
}
