using ChamadoFacil.Models.Authentication;
using ChamadoFacil.Models.Categoria;
using ChamadoFacil.Models.Chamado;
using ChamadoFacil.Models.User;
using System.Threading;
using System.Threading.Tasks;

namespace ChamadoFacil.DataAccess
{
    public interface ICommandStack
    {
        ICommandRepository<UsuarioModel> Usuario { get; }
        ICommandRepository<ChamadoModel> Chamado { get; }
        ICommandRepository<CategoriaModel> Categoria { get; }
        ICommandRepository<RefreshTokenModel> RefreshToken { get; }

        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}
