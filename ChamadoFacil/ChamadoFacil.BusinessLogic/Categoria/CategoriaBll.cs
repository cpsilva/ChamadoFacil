using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ChamadoFacil.DataAccess;
using ChamadoFacil.Models.Categoria;

namespace ChamadoFacil.BusinessLogic.Categoria
{
    public class CategoriaBll : ICategoriaBll
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoriaBll(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<CategoriaModel>> ListarAsync(CancellationToken cancellationToken)
        {
            return await _unitOfWork.QueryStack.Categoria.ListarAsync(cancellationToken);
        }
    }
}
