using ChamadoFacil.BusinessLogic.Authentication;
using ChamadoFacil.CrossCutting.Security.Hash;
using ChamadoFacil.CrossCutting.Security.JsonWebToken;
using ChamadoFacil.DataAccess;
using ChamadoFacil.Models.Authentication;
using ChamadoFacil.Models.Models;
using ChamadoFacil.Models.User;
using Microsoft.Extensions.Options;
using NSubstitute;
using System.Linq.Expressions;

namespace ChamadoFacil.BusinessLogic.Test.Authentication
{
    [TestFixture]
    public class AuthenticationBllTests
    {
        private IQueryRepository<UsuarioModel> _usuarioQueryRepository;
        private IQueryRepository<RefreshTokenModel> _refreshTokenQueryRepository;
        private ICommandRepository<RefreshTokenModel> _refreshTokenCommandRepository;
        private ICommandStack _commandStack;
        private IJWT _jwt;
        private IHash _hash;
        private IAuthenticationBll _sut;

        [SetUp]
        public void SetUp()
        {
            var (unitOfWork, queryStack, commandStack) = UnitOfWorkTestFactory.Create();

            _usuarioQueryRepository = Substitute.For<IQueryRepository<UsuarioModel>>();
            _refreshTokenQueryRepository = Substitute.For<IQueryRepository<RefreshTokenModel>>();
            _refreshTokenCommandRepository = Substitute.For<ICommandRepository<RefreshTokenModel>>();

            queryStack.Usuario.Returns(_usuarioQueryRepository);
            queryStack.RefreshToken.Returns(_refreshTokenQueryRepository);
            commandStack.RefreshToken.Returns(_refreshTokenCommandRepository);
            _commandStack = commandStack;

            _jwt = Substitute.For<IJWT>();
            _hash = Substitute.For<IHash>();

            var options = Options.Create(new JwtSettings { RefreshTokenExpirationDays = 7 });

            _sut = new AuthenticationBll(unitOfWork, _jwt, _hash, options);
        }

        [Test]
        public async Task AuthenticateAsync_ValidCredentials_ReturnsUser()
        {
            // Arrange
            var usuario = new UsuarioModel { Id = 1, Email = "caio@example.com", Senha = "hashed" };
            _usuarioQueryRepository.SelecionarAsync(Arg.Any<Expression<Func<UsuarioModel, bool>>>(), Arg.Any<CancellationToken>())
                .Returns(usuario);
            _hash.Verify("hashed", "plaintext").Returns(true);

            var authentication = new AuthenticationModel { Login = "caio@example.com", Password = "plaintext" };

            // Act
            var result = await _sut.AuthenticateAsync(authentication, CancellationToken.None);

            // Assert
            Assert.That(result, Is.SameAs(usuario));
        }

        [Test]
        public async Task AuthenticateAsync_UnknownEmail_ReturnsNull()
        {
            // Arrange
            _usuarioQueryRepository.SelecionarAsync(Arg.Any<Expression<Func<UsuarioModel, bool>>>(), Arg.Any<CancellationToken>())
                .Returns((UsuarioModel)null);

            var authentication = new AuthenticationModel { Login = "nobody@example.com", Password = "plaintext" };

            // Act
            var result = await _sut.AuthenticateAsync(authentication, CancellationToken.None);

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task AuthenticateAsync_WrongPassword_ReturnsNull()
        {
            // Arrange
            var usuario = new UsuarioModel { Id = 1, Email = "caio@example.com", Senha = "hashed" };
            _usuarioQueryRepository.SelecionarAsync(Arg.Any<Expression<Func<UsuarioModel, bool>>>(), Arg.Any<CancellationToken>())
                .Returns(usuario);
            _hash.Verify("hashed", "wrong").Returns(false);

            var authentication = new AuthenticationModel { Login = "caio@example.com", Password = "wrong" };

            // Act
            var result = await _sut.AuthenticateAsync(authentication, CancellationToken.None);

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task IssueTokensAsync_ReturnsTokenPairAndPersistsRefreshToken()
        {
            // Arrange
            var usuario = new UsuarioModel { Id = 1, Nome = "Caio", Email = "caio@example.com" };
            _jwt.Encode("1", null, "Caio").Returns("access-token");

            // Act
            var result = await _sut.IssueTokensAsync(usuario, CancellationToken.None);

            // Assert
            Assert.That(result.AccessToken, Is.EqualTo("access-token"));
            Assert.That(result.RefreshToken, Is.Not.Null.And.Not.Empty);
            _refreshTokenCommandRepository.Received(1).Adicionar(Arg.Is<RefreshTokenModel>(r => r.UserId == 1 && r.RevokedAt == null));
            await _commandStack.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
        }

        [Test]
        public async Task RefreshAsync_UnknownToken_ReturnsNull()
        {
            // Arrange
            _refreshTokenQueryRepository.SelecionarAsync(Arg.Any<Expression<Func<RefreshTokenModel, bool>>>(), Arg.Any<CancellationToken>())
                .Returns((RefreshTokenModel)null);

            // Act
            var result = await _sut.RefreshAsync("some-token", CancellationToken.None);

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task RefreshAsync_RevokedToken_ReturnsNull()
        {
            // Arrange
            var stored = new RefreshTokenModel { Id = 1, UserId = 1, RevokedAt = DateTime.UtcNow.AddMinutes(-1), ExpiresAt = DateTime.UtcNow.AddDays(1) };
            _refreshTokenQueryRepository.SelecionarAsync(Arg.Any<Expression<Func<RefreshTokenModel, bool>>>(), Arg.Any<CancellationToken>())
                .Returns(stored);

            // Act
            var result = await _sut.RefreshAsync("some-token", CancellationToken.None);

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task RefreshAsync_ExpiredToken_ReturnsNull()
        {
            // Arrange
            var stored = new RefreshTokenModel { Id = 1, UserId = 1, RevokedAt = null, ExpiresAt = DateTime.UtcNow.AddMinutes(-1) };
            _refreshTokenQueryRepository.SelecionarAsync(Arg.Any<Expression<Func<RefreshTokenModel, bool>>>(), Arg.Any<CancellationToken>())
                .Returns(stored);

            // Act
            var result = await _sut.RefreshAsync("some-token", CancellationToken.None);

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task RefreshAsync_ValidToken_RevokesOldTokenAndIssuesNewPair()
        {
            // Arrange
            var stored = new RefreshTokenModel { Id = 1, UserId = 1, RevokedAt = null, ExpiresAt = DateTime.UtcNow.AddDays(1) };
            var usuario = new UsuarioModel { Id = 1, Nome = "Caio", Email = "caio@example.com" };

            _refreshTokenQueryRepository.SelecionarAsync(Arg.Any<Expression<Func<RefreshTokenModel, bool>>>(), Arg.Any<CancellationToken>())
                .Returns(stored);
            _usuarioQueryRepository.SelecionarAsync(1, Arg.Any<CancellationToken>()).Returns(usuario);
            _jwt.Encode("1", null, "Caio").Returns("new-access-token");

            // Act
            var result = await _sut.RefreshAsync("old-raw-token", CancellationToken.None);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.AccessToken, Is.EqualTo("new-access-token"));
            Assert.That(stored.RevokedAt, Is.Not.Null);
            _refreshTokenCommandRepository.Received(1).Atualizar(stored);
            _refreshTokenCommandRepository.Received(1).Adicionar(Arg.Is<RefreshTokenModel>(r => r.UserId == 1));
            await _commandStack.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
        }

        [Test]
        public async Task LogoutAsync_RevokesMatchingToken()
        {
            // Arrange
            var stored = new RefreshTokenModel { Id = 1, UserId = 1, RevokedAt = null, ExpiresAt = DateTime.UtcNow.AddDays(1) };
            _refreshTokenQueryRepository.SelecionarAsync(Arg.Any<Expression<Func<RefreshTokenModel, bool>>>(), Arg.Any<CancellationToken>())
                .Returns(stored);

            // Act
            await _sut.LogoutAsync("some-token", CancellationToken.None);

            // Assert
            Assert.That(stored.RevokedAt, Is.Not.Null);
            _refreshTokenCommandRepository.Received(1).Atualizar(stored);
            await _commandStack.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
        }

        [Test]
        public async Task LogoutAsync_AlreadyRevoked_DoesNothing()
        {
            // Arrange
            var stored = new RefreshTokenModel { Id = 1, UserId = 1, RevokedAt = DateTime.UtcNow.AddMinutes(-5), ExpiresAt = DateTime.UtcNow.AddDays(1) };
            _refreshTokenQueryRepository.SelecionarAsync(Arg.Any<Expression<Func<RefreshTokenModel, bool>>>(), Arg.Any<CancellationToken>())
                .Returns(stored);

            // Act
            await _sut.LogoutAsync("some-token", CancellationToken.None);

            // Assert
            _refreshTokenCommandRepository.DidNotReceive().Atualizar(Arg.Any<RefreshTokenModel>());
            await _commandStack.DidNotReceive().SaveChangesAsync(Arg.Any<CancellationToken>());
        }
    }
}
