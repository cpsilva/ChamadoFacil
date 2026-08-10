using ChamadoFacil.DataAccess.Database.Context;
using ChamadoFacil.DependencyInjection;
using ChamadoFacil.Models.User;
using NUnit.Framework;
using System.Threading;
using System.Threading.Tasks;

namespace ChamadoFacil.DataAccess.Test
{
    [TestFixture]
    public class UserTest
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
        public async Task Adicionar_PersistsUsuarioWithGeneratedId()
        {
            // Arrange
            var usuario = new UsuarioModel
            {
                Nome = "Caio Pires",
                Email = "caio.pires@gmail.com",
                Senha = "q1w2e3r4"
            };

            // Act
            _unitOfWork.CommandStack.Usuario.Adicionar(usuario);
            await _unitOfWork.CommandStack.SaveChangesAsync(CancellationToken.None);

            // Assert
            var persisted = await _unitOfWork.QueryStack.Usuario.SelecionarAsync(usuario.Id, CancellationToken.None);
            Assert.That(persisted, Is.Not.Null);
            Assert.That(persisted.Email, Is.EqualTo("caio.pires@gmail.com"));
            Assert.That(usuario.Id, Is.GreaterThan(0));
        }

        [Test]
        public async Task ExisteAsync_ReturnsFalseForUnknownEmail()
        {
            // Arrange / Act
            var exists = await _unitOfWork.QueryStack.Usuario.ExisteAsync(u => u.Email == "nobody@example.com", CancellationToken.None);

            // Assert
            Assert.That(exists, Is.False);
        }
    }
}
