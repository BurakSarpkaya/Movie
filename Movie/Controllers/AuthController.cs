using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Abstract;
using Entities.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Movie.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IAuthAppService _authAppService;

        public AuthController(IAuthAppService authAppService)
        {
            _authAppService = authAppService;
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] UserForLoginDto userForLoginDto)
        {
            var userToLogin = _authAppService.Login(userForLoginDto);

            if (userToLogin.Success != true)
                return BadRequest(userToLogin.Message);

            var result = _authAppService.CreateAccessToken(userToLogin.Data);

            if (result.Success != true)
                return BadRequest(result.Message);

            return Ok(result.Data);
        }

        [HttpPost("Register")]
        public IActionResult Register([FromBody] UserForRegisterDto userForRegisterDto)
        {

            var registerResult = _authAppService.Register(userForRegisterDto);
            var result = _authAppService.CreateAccessToken(registerResult.Data);

            if (result.Success != true)
                return BadRequest(result.Message);

            return Ok(result.Data);
        }
    }
}
