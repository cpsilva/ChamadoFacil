using ChamadoFacil.Models.Models;

namespace ChamadoFacil.BusinessLogic.Authentication
{
    public interface IAuthenticationBll
    {
        AuthenticatedModel Authenticate(AuthenticationModel authentication);

        string GenerateJwt(AuthenticatedModel authenticated);

        void Logout(long userId);
    }
}