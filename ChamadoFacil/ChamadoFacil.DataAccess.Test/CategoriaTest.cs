using ChamadoFacil.DataAccess.Database.Context;
using ChamadoFacil.DependencyInjection;
using ChamadoFacil.Models.Categoria;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChamadoFacil.DataAccess.Test
{
    [TestClass]
    public class CategoriaTest
    {
        [TestMethod]
        public void CategoriaTeste()
        {
            Container.RegisterServices();
            Container.AddDbContextInMemoryDatabase<DatabaseContext>();
            IUnitOfWork unit = Container.GetService<IUnitOfWork>();

            CategoriaModel c = new CategoriaModel
            {
                Id = 1,
                Descricao = "Teste"
            };

            unit.CommandStack.Categoria.Adicionar(c);
            unit.CommandStack.SaveChanges();
        }
    }
}