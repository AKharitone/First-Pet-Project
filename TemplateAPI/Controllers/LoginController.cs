using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TemplateAPIServices.Interfaces;
using TemplateAPIServices.PublicModels;
using TemplateAPIServices.Services;
using System.Threading.Tasks;

namespace TemplateAPI.Controllers
{
    [AllowAnonymous]
    [Route("login")]
    public class LoginController : Controller
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (model == null)
                return BadRequest();

            try
            {
                var token = await _loginService.CreateJwtToken(model);
                return Ok(token);
            }
            catch (InvalidLoginException)
            {
                return Unauthorized();
            }
            catch (AccountLockedException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}