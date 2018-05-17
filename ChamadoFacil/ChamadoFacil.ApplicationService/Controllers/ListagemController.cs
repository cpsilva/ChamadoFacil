using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChamadoFacil.BusinessLogic.Chamado;
using ChamadoFacil.Models.Chamado;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ChamadoFacil.ApplicationService.Controllers
{
    [Route("[controller]")]
    public class ListagemController : Controller
    {
        private readonly IChamadoBll _chamadoBll;

        public ListagemController(IChamadoBll chamadoBll)
        {
            _chamadoBll = chamadoBll;
        }

        [HttpGet]
        public IList<ChamadoModel> Get()
        {
            return _chamadoBll.Listar();
        }

        [HttpDelete]
        public void Delete(int id)
        {
            _chamadoBll.Apagar(id);
        }
    }
}