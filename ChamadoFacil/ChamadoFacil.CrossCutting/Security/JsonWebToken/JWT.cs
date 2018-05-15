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
        private readonly SymmetricSecurityKey _securityKey;
        private readonly SigningCredentials _signingCredentials;

        public Jwt()
        {
            _securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JWTSettings.SecurityKey));
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
                JWTSettings.Issuer,
                JWTSettings.Audience,
                claims,
                DateTime.UtcNow,
                DateTime.UtcNow.AddDays(JWTSettings.ExpirationTime),
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
                ValidAudience = JWTSettings.Audience,
                ValidIssuer = JWTSettings.Issuer
            };
        }
    }
}