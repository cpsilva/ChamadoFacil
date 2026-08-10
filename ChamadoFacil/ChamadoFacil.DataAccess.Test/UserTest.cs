using ChamadoFacil.DataAccess.Database.Context;
using ChamadoFacil.DependencyInjection;
using ChamadoFacil.Models.User;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using System.Threading.Tasks;

namespace ChamadoFacil.DataAccess.Test
{
    [TestClass]
    public class UserTest
    {
        [TestMethod]
        public async Task GerarTabela()
        {
            Container.RegisterServices();
            Container.AddDbContextInMemoryDatabase<DatabaseContext>();
            IUnitOfWork unit = Container.GetService<IUnitOfWork>();

            UsuarioModel u = new UsuarioModel
            {
                Nome = "Caio Pires",
                Email = "caio.pires@gmail.com",
                Senha = "q1w2e3r4"
            };

            unit.CommandStack.Usuario.Adicionar(u);
            await unit.CommandStack.SaveChangesAsync(CancellationToken.None);
        }
    }
}