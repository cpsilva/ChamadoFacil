using ChamadoFacil.Models.Chamado;
using System.Collections.Generic;

namespace ChamadoFacil.BusinessLogic.Chamado
{
    public interface IChamadoBll
    {
        ChamadoModel Selecionar(int id);

        IList<ChamadoModel> Listar();

        void Apagar(int id);

        void Salvar(ChamadoModel chamadoModel);
    }
}