using ChamadoFacil.DataAccess.Database.Context;
using ChamadoFacil.DependencyInjection;
using ChamadoFacil.Models.Categoria;
using NUnit.Framework;
using System.Threading;
using System.Threading.Tasks;

namespace ChamadoFacil.DataAccess.Test
{
    [TestFixture]
    public class CategoriaTest
    {
        private IUnitOfWork _unitOfWork;

        [SetUp]
        public void SetUp()
        {
            Container.Reset();
            Container.RegisterServices();
            Container.AddDbContextInMemoryDatabase<DatabaseContext>();
            _unitOfWork = Container.GetService<IUnitOfWork>();
        }

        [Test]
        public async Task Adicionar_PersistsCategoriaWithGeneratedId()
        {
            // Arrange
            var categoria = new CategoriaModel { Descricao = "Hardware" };

            // Act
            _unitOfWork.CommandStack.Categoria.Adicionar(categoria);
            await _unitOfWork.CommandStack.SaveChangesAsync(CancellationToken.None);

            // Assert
            var persisted = await _unitOfWork.QueryStack.Categoria.SelecionarAsync(categoria.Id, CancellationToken.None);
            Assert.That(persisted, Is.Not.Null);
            Assert.That(persisted.Descricao, Is.EqualTo("Hardware"));
            Assert.That(categoria.Id, Is.GreaterThan(0));
        }

        [Test]
        public async Task ListarAsync_ReturnsAllPersistedCategorias()
        {
            // Arrange
            _unitOfWork.CommandStack.Categoria.Adicionar(new CategoriaModel { Descricao = "Hardware" });
            _unitOfWork.CommandStack.Categoria.Adicionar(new CategoriaModel { Descricao = "Software" });
            await _unitOfWork.CommandStack.SaveChangesAsync(CancellationToken.None);

            // Act
            var categorias = await _unitOfWork.QueryStack.Categoria.ListarAsync(CancellationToken.None);

            // Assert
            Assert.That(categorias, Has.Count.EqualTo(2));
        }
    }
}
