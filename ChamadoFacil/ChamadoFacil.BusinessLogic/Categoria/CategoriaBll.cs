using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public IEnumerable<CategoriaModel> Listar(CategoriaModel categoria)
        {
            return _unitOfWork.QueryStack.Categoria.Listar();
        }
    }
}