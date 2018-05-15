using Microsoft.IdentityModel.Tokens;

namespace ChamadoFacil.CrossCutting.Security.JsonWebToken
{
    public interface IJWT
    {
        T Decode<T>(string token);

        string Encode(string sub, string[] roles = null, object data = null);

        TokenValidationParameters GetTokenValidationParameters();
    }
}