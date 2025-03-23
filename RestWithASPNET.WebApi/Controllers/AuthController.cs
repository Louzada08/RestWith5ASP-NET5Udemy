using Asp.Versioning;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestWithASPNET.Application.Interfaces.Users;
using RestWithASPNET.Application.Services.Login;
using RestWithASPNET.Application.Services.Token;
using RestWithASPNET.Domain.ValueObjects;

namespace RestWithASPNET.WebApi.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/[controller]/v{version:apiVersion}")]
    public class AuthController : MainController
    {
        private readonly TokenService _tokenService;
        private readonly IUserService _userService2;
        private readonly ILoginService _userService;

        public AuthController(IMapper mapper, IMediator mediator, TokenService tokenService,
                 IUserService userService2, ILoginService userService) : base(mapper, mediator)
        {
            _userService = userService;
            _tokenService = tokenService;
        }

        /// <summary>
        /// Gera um novo token para o usuário
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("signin")]
        public IActionResult Signin([FromBody] UserVO user)
        {
            if (user == null) return BadRequest("Solicitação de cliente invalida");
            var token = _userService.ValidateCredentials(user);
            if (token == null) return Unauthorized();
            return Ok(token);
        }

        /// <summary>
        /// Refresh token
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("refresh")]
        public IActionResult Refresh([FromBody] TokenVO tokenVo)
        {
            if (tokenVo is null) return BadRequest("Solicitação de cliente inválida");
            //var token = _loginService.ValidateCredentials(tokenVo);
           // if (token == null) return BadRequest("Solicitação de cliente inválida");
            return Ok();
        }

        /// <summary>
        /// Cancela o token do usuário
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("revoke")]
        [Authorize("Bearer")]
        public IActionResult Revoke()
        {
            var username = User.Identity.Name;
           // var result = _loginService.RevokeToken(username);

           // if (!result) return BadRequest("Solicitação de cliente inválida");
            return NoContent();
        }
    }
}
