using ChamadoFacil.Models.Models;
using System;

namespace ChamadoFacil.BusinessLogic.Authentication
{
    public class AuthenticationBll : IAuthenticationBll
    {
        public AuthenticatedModel Authenticate(AuthenticationModel authentication)
        {
            throw new NotImplementedException();
        }

        public string GenerateJwt(AuthenticatedModel authenticated)
        {
            throw new NotImplementedException();
        }

        public void Logout(long userId)
        {
            throw new NotImplementedException();
        }
    }
}