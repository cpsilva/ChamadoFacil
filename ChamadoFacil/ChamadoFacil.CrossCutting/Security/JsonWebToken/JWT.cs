using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace ChamadoFacil.CrossCutting.Security.JsonWebToken
{
    public class Jwt : IJWT
    {
        private readonly JwtSettings _settings;
        private readonly SymmetricSecurityKey _securityKey;
        private readonly SigningCredentials _signingCredentials;

        public Jwt(IOptions<JwtSettings> options)
        {
            _settings = options.Value;
            _securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.SecurityKey));
            _signingCredentials = new SigningCredentials(_securityKey, SecurityAlgorithms.HmacSha512);
        }

        public T Decode<T>(string token)
        {
            var decoded = new JwtSecurityTokenHandler().ReadJwtToken(token);
            var payload = decoded.Payload[typeof(T).Name].ToString();
            return JsonConvert.DeserializeObject<T>(payload);
        }

        public string Encode(string sub, string[] roles = null, object data = null)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, sub),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            roles?.ToList().ForEach(role => claims.Add(new Claim(ClaimTypes.Role, role)));

            if (data != null)
            {
                claims.Add(new Claim(ClaimTypes.UserData, JsonConvert.SerializeObject(data)));
            }

            var jwtSecurityToken = new JwtSecurityToken
            (
                _settings.Issuer,
                _settings.Audience,
                claims,
                DateTime.UtcNow,
                DateTime.UtcNow.AddMinutes(_settings.AccessTokenExpirationMinutes),
                _signingCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        }

        public TokenValidationParameters GetTokenValidationParameters()
        {
            return new TokenValidationParameters
            {
                IssuerSigningKey = _securityKey,
                ValidateActor = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true,
                ValidAudience = _settings.Audience,
                ValidIssuer = _settings.Issuer
            };
        }
    }
}
