using ChamadoFacil.Models.Categoria;
using System.Collections.Generic;

namespace ChamadoFacil.BusinessLogic.Categoria
{
    public interface ICategoriaBll
    {
        IEnumerable<CategoriaModel> Listar(CategoriaModel categoria);
    }
}