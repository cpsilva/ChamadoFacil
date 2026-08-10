using ChamadoFacil.DataAccess.Database.Context;
using ChamadoFacil.DependencyInjection;
using ChamadoFacil.Models.Categoria;
using ChamadoFacil.Models.Chamado;
using ChamadoFacil.Models.User;
using NUnit.Framework;
using System.Threading;
using System.Threading.Tasks;

namespace ChamadoFacil.DataAccess.Test
{
    [TestFixture]
    public class ChamadoTest
    {
        private IUnitOfWork _unitOfWork;
        private int _categoriaId;
        private int _usuarioId;

        [SetUp]
        public async Task SetUp()
        {
            Container.Reset();
            Container.RegisterServices();
            Container.AddDbContextInMemoryDatabase<DatabaseContext>();
            _unitOfWork = Container.GetService<IUnitOfWork>();

            var categoria = new CategoriaModel { Descricao = "Hardware" };
            _unitOfWork.CommandStack.Categoria.Adicionar(categoria);

            var usuario = new UsuarioModel { Nome = "Caio", Email = "caio@example.com", Senha = "hash" };
            _unitOfWork.CommandStack.Usuario.Adicionar(usuario);

            await _unitOfWork.CommandStack.SaveChangesAsync(CancellationToken.None);

            _categoriaId = categoria.Id;
            _usuarioId = usuario.Id;
        }

        [Test]
        public async Task Adicionar_PersistsChamadoWithGeneratedId()
        {
            // Arrange
            var chamado = new ChamadoModel
            {
                Titulo = "Teste",
                Mensagem = "Teste Mensagem",
                Urgencia = 1,
                CategoriaId = _categoriaId,
                UsuarioId = _usuarioId
            };

            // Act
            _unitOfWork.CommandStack.Chamado.Adicionar(chamado);
            await _unitOfWork.CommandStack.SaveChangesAsync(CancellationToken.None);

            // Assert
            var persisted = await _unitOfWork.QueryStack.Chamado.SelecionarAsync(chamado.Id, CancellationToken.None);
            Assert.That(persisted, Is.Not.Null);
            Assert.That(persisted.Titulo, Is.EqualTo("Teste"));
            Assert.That(persisted.Urgencia, Is.EqualTo(1));
        }

        [Test]
        public async Task Atualizar_ModifiesExistingChamadoWithoutCreatingDuplicate()
        {
            // Arrange
            var chamado = new ChamadoModel
            {
                Titulo = "Original",
                Mensagem = "Mensagem original",
                Urgencia = 1,
                CategoriaId = _categoriaId,
                UsuarioId = _usuarioId
            };
            _unitOfWork.CommandStack.Chamado.Adicionar(chamado);
            await _unitOfWork.CommandStack.SaveChangesAsync(CancellationToken.None);

            // Act
            chamado.Titulo = "Atualizado";
            _unitOfWork.CommandStack.Chamado.Atualizar(chamado);
            await _unitOfWork.CommandStack.SaveChangesAsync(CancellationToken.None);

            // Assert
            var todos = await _unitOfWork.QueryStack.Chamado.ListarAsync(CancellationToken.None);
            Assert.That(todos, Has.Count.EqualTo(1));
            Assert.That(todos[0].Titulo, Is.EqualTo("Atualizado"));
        }

        [Test]
        public async Task ApagarAsync_RemovesChamado()
        {
            // Arrange
            var chamado = new ChamadoModel
            {
                Titulo = "Para apagar",
                Mensagem = "Mensagem",
                Urgencia = 1,
                CategoriaId = _categoriaId,
                UsuarioId = _usuarioId
            };
            _unitOfWork.CommandStack.Chamado.Adicionar(chamado);
            await _unitOfWork.CommandStack.SaveChangesAsync(CancellationToken.None);

            // Act
            await _unitOfWork.CommandStack.Chamado.ApagarAsync(chamado.Id, CancellationToken.None);
            await _unitOfWork.CommandStack.SaveChangesAsync(CancellationToken.None);

            // Assert
            var persisted = await _unitOfWork.QueryStack.Chamado.SelecionarAsync(chamado.Id, CancellationToken.None);
            Assert.That(persisted, Is.Null);
        }
    }
}
