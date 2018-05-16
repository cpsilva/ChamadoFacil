using ChamadoFacil.BusinessLogic.Chamado;
using ChamadoFacil.Models.Chamado;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ChamadoFacil.ApplicationService.Controllers
{
    [Route("[controller]")]
    [Authorize]
    public class ChamadoController : Controller
    {
        private readonly IChamadoBll _chamadoBll;

        public ChamadoController(IChamadoBll chamadoBll)
        {
            _chamadoBll = chamadoBll;
        }

        public IList<ChamadoModel> Get()
        {
            return _chamadoBll.Listar();
        }

        public ChamadoModel Get(int id)
        {
            return _chamadoBll.Selecionar(id);
        }

        public void Post([FromBody]ChamadoModel chamadoModel)
        {
            _chamadoBll.Salvar(chamadoModel);
        }

        public void Delete(int id)
        {
            _chamadoBll.Apagar(id);
        }
    }
}