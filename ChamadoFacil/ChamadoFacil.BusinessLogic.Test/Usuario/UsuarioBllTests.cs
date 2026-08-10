using ChamadoFacil.BusinessLogic.Usuario;
using ChamadoFacil.CrossCutting.Security.Hash;
using ChamadoFacil.DataAccess;
using ChamadoFacil.Models.User;
using NSubstitute;

namespace ChamadoFacil.BusinessLogic.Test.Usuario
{
    [TestFixture]
    public class UsuarioBllTests
    {
        private ICommandRepository<UsuarioModel> _commandRepository;
        private ICommandStack _commandStack;
        private IHash _hash;
        private IUsuarioBll _sut;

        [SetUp]
        public void SetUp()
        {
            var (unitOfWork, _, commandStack) = UnitOfWorkTestFactory.Create();

            _commandRepository = Substitute.For<ICommandRepository<UsuarioModel>>();
            commandStack.Usuario.Returns(_commandRepository);
            _commandStack = commandStack;

            _hash = Substitute.For<IHash>();

            _sut = new UsuarioBll(unitOfWork, _hash);
        }

        [Test]
        public async Task CadastrarAsync_HashesPasswordBeforePersisting()
        {
            // Arrange
            var usuario = new UsuarioModel { Nome = "Caio", Email = "caio@example.com", Senha = "plaintext" };
            _hash.Generate("plaintext").Returns("hashed-value");

            // Act
            await _sut.CadastrarAsync(usuario, CancellationToken.None);

            // Assert
            Assert.That(usuario.Senha, Is.EqualTo("hashed-value"));
        }

        [Test]
        public async Task CadastrarAsync_AddsAndSaves()
        {
            // Arrange
            var usuario = new UsuarioModel { Nome = "Caio", Email = "caio@example.com", Senha = "plaintext" };
            _hash.Generate(Arg.Any<string>()).Returns("hashed-value");

            // Act
            await _sut.CadastrarAsync(usuario, CancellationToken.None);

            // Assert
            _commandRepository.Received(1).Adicionar(usuario);
            await _commandStack.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
        }
    }
}
