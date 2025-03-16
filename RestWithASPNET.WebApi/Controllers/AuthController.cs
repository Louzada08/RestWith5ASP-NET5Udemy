using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestWithASPNET.Domain.ValueObjects;

namespace RestWithASPNET.WebApi.Controllers
{
    [ApiVersion("2")]
    [ApiController]
    [Route("api/[controller]/v{version:apiVersion}")]
    public class AuthController : ControllerBase
    {
        private ILoginService _loginService;

        public AuthController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost]
        [Route("signin")]
        public IActionResult Signin([FromBody] UserVO user)
        {
            if (user == null) return BadRequest("Solicitação de cliente invalida");
            var token = _loginService.ValidateCredentials(user);
            if (token == null) return Unauthorized();
            return Ok(token);
        }

        [HttpPost]
        [Route("refresh")]
        public IActionResult Refresh([FromBody] TokenVO tokenVo)
        {
            if (tokenVo is null) return BadRequest("Solicitação de cliente inválida");
            var token = _loginService.ValidateCredentials(tokenVo);
            if (token == null) return BadRequest("Solicitação de cliente inválida");
            return Ok(token);
        }


        [HttpGet]
        [Route("revoke")]
        [Authorize("Bearer")]
        public IActionResult Revoke()
        {
            var username = User.Identity.Name;
            var result = _loginService.RevokeToken(username);

            if (!result) return BadRequest("Solicitação de cliente inválida");
            return NoContent();
        }
    }
}
