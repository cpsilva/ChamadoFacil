using ChamadoFacil.Models.Categoria;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ChamadoFacil.BusinessLogic.Categoria
{
    public interface ICategoriaBll
    {
        Task<IEnumerable<CategoriaModel>> ListarAsync(CancellationToken cancellationToken);
    }
}
