using ChamadoFacil.BusinessLogic.Categoria;
using ChamadoFacil.DataAccess;
using ChamadoFacil.Models.Categoria;
using NSubstitute;

namespace ChamadoFacil.BusinessLogic.Test.Categoria
{
    [TestFixture]
    public class CategoriaBllTests
    {
        [Test]
        public async Task ListarAsync_ReturnsAllFromRepository()
        {
            // Arrange
            var (unitOfWork, queryStack, _) = UnitOfWorkTestFactory.Create();
            var queryRepository = Substitute.For<IQueryRepository<CategoriaModel>>();
            var categorias = new List<CategoriaModel> { new() { Id = 1, Descricao = "Hardware" } };
            queryRepository.ListarAsync(Arg.Any<CancellationToken>()).Returns(categorias);
            queryStack.Categoria.Returns(queryRepository);

            var sut = new CategoriaBll(unitOfWork);

            // Act
            var result = await sut.ListarAsync(CancellationToken.None);

            // Assert
            Assert.That(result, Is.EquivalentTo(categorias));
        }
    }
}
