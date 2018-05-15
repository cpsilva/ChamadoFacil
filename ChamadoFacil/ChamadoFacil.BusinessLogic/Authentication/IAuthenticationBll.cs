using ChamadoFacil.Models.Models;
using ChamadoFacil.Models.User;

namespace ChamadoFacil.BusinessLogic.Authentication
{
    public interface IAuthenticationBll
    {
        UsuarioModel Authenticate(AuthenticationModel authentication);

        string GenerateJwt(UsuarioModel authenticated);

        void Logout(long userId);
    }
}