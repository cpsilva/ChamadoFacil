using ChamadoFacil.DataAccess.Database.Context;
using ChamadoFacil.DependencyInjection;
using ChamadoFacil.Models.User;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChamadoFacil.DataAccess.Test
{
    [TestClass]
    public class UserTest
    {
        [TestMethod]
        public void GerarTabela()
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

            //using (var context = new DatabaseContext())
            //{
            //    context.Database.EnsureCreated();
            //    context.Database.Migrate();
            //    context.Usuarios.Add(u);
            //    context.SaveChanges();
            //}

            unit.CommandStack.Usuario.Adicionar(u);
            unit.CommandStack.SaveChanges();
        }
    }
}