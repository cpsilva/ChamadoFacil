using ChamadoFacil.BusinessLogic.Chamado;
using ChamadoFacil.Models.Chamado;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ChamadoFacil.ApplicationService.Controllers
{
    [Route("[controller]")]
    public class ChamadoController : Controller
    {
        private readonly IChamadoBll _chamadoBll;

        public ChamadoController(IChamadoBll chamadoBll)
        {
            _chamadoBll = chamadoBll;
        }

        [HttpGet]
        public ChamadoModel Get(int id)
        {
            return _chamadoBll.Selecionar(id);
        }

        [HttpPost]
        public void Post([FromBody]ChamadoModel chamadoModel)
        {
            _chamadoBll.Salvar(chamadoModel);
        }
    }
}