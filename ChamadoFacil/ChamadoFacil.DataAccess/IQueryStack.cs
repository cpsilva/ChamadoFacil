using ChamadoFacil.Models.User;

namespace ChamadoFacil.DataAccess
{
    public interface IQueryStack
    {
        IQueryRepository<Usuario> Usuario { get; }
    }
}