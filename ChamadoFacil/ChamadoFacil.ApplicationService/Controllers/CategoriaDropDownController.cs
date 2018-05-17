using ChamadoFacil.BusinessLogic.Categoria;
using ChamadoFacil.Models.Categoria;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ChamadoFacil.ApplicationService.Controllers
{
    [Route("[controller]")]
    public class CategoriaDropDownController : Controller
    {
        private readonly ICategoriaBll _categoria;

        public CategoriaDropDownController(ICategoriaBll categoria)
        {
            _categoria = categoria;
        }

        [HttpGet]
        public IEnumerable<KeyValuePair<int, string>> Get(CategoriaModel categoria)
        {
            return _categoria
                .Listar(categoria)
                .OrderBy(o => o.Descricao)
                .Select(s => new KeyValuePair<int, string>(s.Id, s.Descricao));
        }
    }
}