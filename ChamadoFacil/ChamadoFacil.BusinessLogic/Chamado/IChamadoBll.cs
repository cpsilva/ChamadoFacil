using ChamadoFacil.Models.Chamado;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ChamadoFacil.BusinessLogic.Chamado
{
    public interface IChamadoBll
    {
        Task<ChamadoModel> SelecionarAsync(int id, CancellationToken cancellationToken);

        Task<IList<ChamadoModel>> ListarAsync(CancellationToken cancellationToken);

        Task ApagarAsync(int id, CancellationToken cancellationToken);

        Task SalvarAsync(ChamadoModel chamadoModel, CancellationToken cancellationToken);
    }
}
