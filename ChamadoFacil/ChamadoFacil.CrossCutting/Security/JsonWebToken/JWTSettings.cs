using System;

namespace ChamadoFacil.CrossCutting.Security.JsonWebToken
{
    public class JWTSettings
    {
        public static string Audience { get; set; } = nameof(Audience);

        public static int ExpirationTime { get; set; } = 1;

        public static string Issuer { get; set; } = nameof(Issuer);

        public static string SecurityKey { get; } = Guid.NewGuid() + DateTime.UtcNow.ToString();
    }
}