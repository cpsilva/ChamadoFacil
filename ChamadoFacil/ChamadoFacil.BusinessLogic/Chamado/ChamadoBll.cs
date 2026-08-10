using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ChamadoFacil.DataAccess;
using ChamadoFacil.Models.Chamado;

namespace ChamadoFacil.BusinessLogic.Chamado
{
    public class ChamadoBll : IChamadoBll
    {
        private readonly IUnitOfWork _unitOfWork;

        public ChamadoBll(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task ApagarAsync(int id, CancellationToken cancellationToken)
        {
            await _unitOfWork.CommandStack.Chamado.ApagarAsync(id, cancellationToken);
            await _unitOfWork.CommandStack.SaveChangesAsync(cancellationToken);
        }

        public async Task<IList<ChamadoModel>> ListarAsync(CancellationToken cancellationToken)
        {
            return await _unitOfWork.QueryStack.Chamado.ListarAsync(cancellationToken);
        }

        public async Task SalvarAsync(ChamadoModel chamadoModel, CancellationToken cancellationToken)
        {
            if (chamadoModel.Id > 0)
            {
                _unitOfWork.CommandStack.Chamado.Atualizar(chamadoModel);
            }
            else
            {
                _unitOfWork.CommandStack.Chamado.Adicionar(chamadoModel);
            }

            await _unitOfWork.CommandStack.SaveChangesAsync(cancellationToken);
        }

        public Task<ChamadoModel> SelecionarAsync(int id, CancellationToken cancellationToken)
        {
            return _unitOfWork.QueryStack.Chamado.SelecionarAsync(id, cancellationToken);
        }
    }
}
