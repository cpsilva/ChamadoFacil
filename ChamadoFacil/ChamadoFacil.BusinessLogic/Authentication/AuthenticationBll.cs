using ChamadoFacil.CrossCutting.Security.Hash;
using ChamadoFacil.CrossCutting.Security.JsonWebToken;
using ChamadoFacil.DataAccess;
using ChamadoFacil.Models.Authentication;
using ChamadoFacil.Models.Models;
using ChamadoFacil.Models.User;
using Microsoft.Extensions.Options;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChamadoFacil.BusinessLogic.Authentication
{
    public class AuthenticationBll : IAuthenticationBll
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJWT _jwt;
        private readonly IHash _hash;
        private readonly JwtSettings _settings;

        public AuthenticationBll(IUnitOfWork unitOfWork, IJWT jwt, IHash hash, IOptions<JwtSettings> options)
        {
            _unitOfWork = unitOfWork;
            _jwt = jwt;
            _hash = hash;
            _settings = options.Value;
        }

        public async Task<UsuarioModel> AuthenticateAsync(AuthenticationModel authentication, CancellationToken cancellationToken)
        {
            var usuario = await _unitOfWork.QueryStack.Usuario.SelecionarAsync(x => x.Email.Equals(authentication.Login), cancellationToken);

            if (usuario == null || !_hash.Verify(usuario.Senha, authentication.Password))
            {
                return null;
            }

            return usuario;
        }

        public async Task<TokenPairModel> IssueTokensAsync(UsuarioModel usuario, CancellationToken cancellationToken)
        {
            var accessToken = _jwt.Encode(usuario.Id.ToString(), null, usuario.Nome);
            var refreshToken = CreateRefreshToken(usuario.Id);

            await _unitOfWork.CommandStack.SaveChangesAsync(cancellationToken);

            return new TokenPairModel { AccessToken = accessToken, RefreshToken = refreshToken };
        }

        public async Task<TokenPairModel> RefreshAsync(string refreshToken, CancellationToken cancellationToken)
        {
            var tokenHash = HashToken(refreshToken);
            var stored = await _unitOfWork.QueryStack.RefreshToken.SelecionarAsync(x => x.TokenHash == tokenHash, cancellationToken);

            if (stored == null || stored.RevokedAt != null || stored.ExpiresAt <= DateTime.UtcNow)
            {
                return null;
            }

            var usuario = await _unitOfWork.QueryStack.Usuario.SelecionarAsync(stored.UserId, cancellationToken);

            if (usuario == null)
            {
                return null;
            }

            stored.RevokedAt = DateTime.UtcNow;
            _unitOfWork.CommandStack.RefreshToken.Atualizar(stored);

            var accessToken = _jwt.Encode(usuario.Id.ToString(), null, usuario.Nome);
            var newRefreshToken = CreateRefreshToken(usuario.Id);

            await _unitOfWork.CommandStack.SaveChangesAsync(cancellationToken);

            return new TokenPairModel { AccessToken = accessToken, RefreshToken = newRefreshToken };
        }

        public async Task LogoutAsync(string refreshToken, CancellationToken cancellationToken)
        {
            var tokenHash = HashToken(refreshToken);
            var stored = await _unitOfWork.QueryStack.RefreshToken.SelecionarAsync(x => x.TokenHash == tokenHash, cancellationToken);

            if (stored == null || stored.RevokedAt != null)
            {
                return;
            }

            stored.RevokedAt = DateTime.UtcNow;
            _unitOfWork.CommandStack.RefreshToken.Atualizar(stored);

            await _unitOfWork.CommandStack.SaveChangesAsync(cancellationToken);
        }

        private string CreateRefreshToken(int userId)
        {
            var rawToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));

            var refreshTokenModel = new RefreshTokenModel
            {
                UserId = userId,
                TokenHash = HashToken(rawToken),
                ExpiresAt = DateTime.UtcNow.AddDays(_settings.RefreshTokenExpirationDays)
            };

            _unitOfWork.CommandStack.RefreshToken.Adicionar(refreshTokenModel);

            return rawToken;
        }

        private static string HashToken(string token)
        {
            return Convert.ToBase64String(SHA256.HashData(Encoding.UTF8.GetBytes(token)));
        }
    }
}
