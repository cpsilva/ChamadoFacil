using System.Collections.Generic;
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

        public void Apagar(int id)
        {
            _unitOfWork.CommandStack.Chamado.Apagar(id);
            _unitOfWork.CommandStack.SaveChanges();
        }

        public IList<ChamadoModel> Listar()
        {
            return _unitOfWork.QueryStack.Chamado.Listar();
        }

        public void Salvar(ChamadoModel chamadoModel)
        {
            if (chamadoModel.Id > 0)
            {
                _unitOfWork.CommandStack.Chamado.Atualizar(chamadoModel);
            }
            else
            {
                _unitOfWork.CommandStack.Chamado.Adicionar(chamadoModel);
            }

            _unitOfWork.CommandStack.SaveChanges();
        }

        public ChamadoModel Selecionar(int id)
        {
            return _unitOfWork.QueryStack.Chamado.Selecionar(id);
        }
    }
}