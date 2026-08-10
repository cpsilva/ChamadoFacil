using ChamadoFacil.Models.Authentication;
using ChamadoFacil.Models.Models;
using ChamadoFacil.Models.User;
using System.Threading;
using System.Threading.Tasks;

namespace ChamadoFacil.BusinessLogic.Authentication
{
    public interface IAuthenticationBll
    {
        Task<UsuarioModel> AuthenticateAsync(AuthenticationModel authentication, CancellationToken cancellationToken);

        Task<TokenPairModel> IssueTokensAsync(UsuarioModel usuario, CancellationToken cancellationToken);

        Task<TokenPairModel> RefreshAsync(string refreshToken, CancellationToken cancellationToken);

        Task LogoutAsync(string refreshToken, CancellationToken cancellationToken);
    }
}
