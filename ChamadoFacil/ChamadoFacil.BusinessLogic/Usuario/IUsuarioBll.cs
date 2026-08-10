using ChamadoFacil.Models.User;
using System.Threading;
using System.Threading.Tasks;

namespace ChamadoFacil.BusinessLogic.Usuario
{
    public interface IUsuarioBll
    {
        Task CadastrarAsync(UsuarioModel usuario, CancellationToken cancellationToken);
    }
}
