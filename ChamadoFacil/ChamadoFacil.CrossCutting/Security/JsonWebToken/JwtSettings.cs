namespace ChamadoFacil.CrossCutting.Security.JsonWebToken
{
    public class JwtSettings
    {
        public string SecurityKey { get; set; }

        public string Audience { get; set; }

        public string Issuer { get; set; }

        public int AccessTokenExpirationMinutes { get; set; } = 15;
    }
}
