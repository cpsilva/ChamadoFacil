using ChamadoFacil.BusinessLogic.Chamado;
using ChamadoFacil.DataAccess;
using ChamadoFacil.Models.Chamado;
using NSubstitute;

namespace ChamadoFacil.BusinessLogic.Test.Chamado
{
    [TestFixture]
    public class ChamadoBllTests
    {
        private IQueryRepository<ChamadoModel> _queryRepository;
        private ICommandRepository<ChamadoModel> _commandRepository;
        private ICommandStack _commandStack;
        private IChamadoBll _sut;

        [SetUp]
        public void SetUp()
        {
            var (unitOfWork, queryStack, commandStack) = UnitOfWorkTestFactory.Create();

            _queryRepository = Substitute.For<IQueryRepository<ChamadoModel>>();
            _commandRepository = Substitute.For<ICommandRepository<ChamadoModel>>();
            queryStack.Chamado.Returns(_queryRepository);
            commandStack.Chamado.Returns(_commandRepository);
            _commandStack = commandStack;

            _sut = new ChamadoBll(unitOfWork);
        }

        [Test]
        public async Task SelecionarAsync_ReturnsChamadoFromRepository()
        {
            // Arrange
            var chamado = new ChamadoModel { Id = 1, Titulo = "Teste" };
            _queryRepository.SelecionarAsync(1, Arg.Any<CancellationToken>()).Returns(chamado);

            // Act
            var result = await _sut.SelecionarAsync(1, CancellationToken.None);

            // Assert
            Assert.That(result, Is.SameAs(chamado));
        }

        [Test]
        public async Task ListarAsync_ReturnsAllFromRepository()
        {
            // Arrange
            var chamados = new List<ChamadoModel> { new() { Id = 1 }, new() { Id = 2 } };
            _queryRepository.ListarAsync(Arg.Any<CancellationToken>()).Returns(chamados);

            // Act
            var result = await _sut.ListarAsync(CancellationToken.None);

            // Assert
            Assert.That(result, Has.Count.EqualTo(2));
        }

        [Test]
        public async Task SalvarAsync_WithNoId_AddsNewChamado()
        {
            // Arrange
            var chamado = new ChamadoModel { Id = 0, Titulo = "Novo" };

            // Act
            await _sut.SalvarAsync(chamado, CancellationToken.None);

            // Assert
            _commandRepository.Received(1).Adicionar(chamado);
            _commandRepository.DidNotReceive().Atualizar(Arg.Any<ChamadoModel>());
            await _commandStack.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
        }

        [Test]
        public async Task SalvarAsync_WithExistingId_UpdatesChamado()
        {
            // Arrange
            var chamado = new ChamadoModel { Id = 5, Titulo = "Existente" };

            // Act
            await _sut.SalvarAsync(chamado, CancellationToken.None);

            // Assert
            _commandRepository.Received(1).Atualizar(chamado);
            _commandRepository.DidNotReceive().Adicionar(Arg.Any<ChamadoModel>());
            await _commandStack.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
        }

        [Test]
        public async Task ApagarAsync_RemovesAndSaves()
        {
            // Act
            await _sut.ApagarAsync(7, CancellationToken.None);

            // Assert
            await _commandRepository.Received(1).ApagarAsync(7, Arg.Any<CancellationToken>());
            await _commandStack.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
        }
    }
}
