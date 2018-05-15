using ChamadoFacil.Models.Categoria;
using ChamadoFacil.Models.Chamado;
using ChamadoFacil.Models.User;

namespace ChamadoFacil.DataAccess
{
    public interface IQueryStack
    {
        IQueryRepository<UsuarioModel> Usuario { get; }
        IQueryRepository<CategoriaModel> Categoria { get; }
        IQueryRepository<ChamadoModel> Chamado { get; }
    }
}