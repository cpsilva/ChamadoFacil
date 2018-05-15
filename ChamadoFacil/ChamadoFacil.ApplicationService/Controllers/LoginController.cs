using ChamadoFacil.BusinessLogic.Authentication;
using ChamadoFacil.Models.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChamadoFacil.ApplicationService.Controllers
{
    [Route("[controller]")]
    public class LoginController : Controller
    {
        private readonly IAuthenticationBll _authenticationBll;

        public LoginController(IAuthenticationBll authenticationBll)
        {
            _authenticationBll = authenticationBll;
        }

        [AllowAnonymous]
        public IActionResult Post([FromBody]AuthenticationModel authentication)
        {
            var authenticated = _authenticationBll.Authenticate(authentication);
            var jwt = _authenticationBll.GenerateJwt(authenticated);
            return Json(jwt);
        }
    }
}