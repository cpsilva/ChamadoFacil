using ChamadoFacil.BusinessLogic.Usuario;
using ChamadoFacil.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChamadoFacil.ApplicationService.Controllers
{
    [Route("[controller]")]
    public class CadastroController : Controller
    {
        private readonly IUsuarioBll _usuarioBll;

        public CadastroController(IUsuarioBll usuarioBll)
        {
            _usuarioBll = usuarioBll;
        }

        [AllowAnonymous]
        public void Post([FromBody]UsuarioModel usuario)
        {
            _usuarioBll.Cadastrar(usuario);
        }
    }
}