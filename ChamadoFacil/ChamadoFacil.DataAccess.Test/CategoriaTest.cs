using ChamadoFacil.DataAccess.Database.Context;
using ChamadoFacil.DependencyInjection;
using ChamadoFacil.Models.Categoria;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using System.Threading.Tasks;

namespace ChamadoFacil.DataAccess.Test
{
    [TestClass]
    public class CategoriaTest
    {
        [TestMethod]
        public async Task CategoriaTeste()
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
            await unit.CommandStack.SaveChangesAsync(CancellationToken.None);
        }
    }
}