using ChamadoFacil.DataAccess.Database.Context;
using ChamadoFacil.DependencyInjection;
using ChamadoFacil.Models.Chamado;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChamadoFacil.DataAccess.Test
{
    [TestClass]
    public class ChamadoTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            Container.RegisterServices();
            Container.AddDbContextInMemoryDatabase<DatabaseContext>();
            IUnitOfWork unit = Container.GetService<IUnitOfWork>();

            ChamadoModel c = new ChamadoModel
            {
                Titulo = "Teste",
                Mensagem = "Teste Mensagem",
                Urgencia = 1,
                CategoriaId = 1,
                UsuarioId = 1
            };

            unit.CommandStack.Chamado.Adicionar(c);
            unit.CommandStack.SaveChanges();
        }
    }
}