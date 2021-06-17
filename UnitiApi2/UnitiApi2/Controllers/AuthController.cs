using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitiApi2.Models;
using UnitiApi2.Repositories;



namespace UnitiApi2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IAuthRepository _authRepository;

        private IConfiguration _config;

        public AuthController(IAuthRepository authRepository, IConfiguration config)
        {
            _authRepository = authRepository;
            _config = config;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody] Auth auth)
        {
            IActionResult response = Unauthorized();
            var user = _authRepository.AuthUser(auth.Username, auth.Password);

            
            if (user != null)
            {
                var tokenString = GenerateJSONWebToken();
                response = Ok(new { token = tokenString });
            }


            return response;

        }

        [HttpGet]
        public async Task<IEnumerable<Auth>> GetAuths()
        {
            return await _authRepository.Get();
        }

        [HttpPost("postauth")]
        public async Task<ActionResult<Auth>> PostAuth([FromBody] Auth auth)
        {
            var newAuth = await _authRepository.Create(auth);
            return CreatedAtAction(nameof(GetAuths), newAuth);
        }



        //WebToken Generation
        private string GenerateJSONWebToken()
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                null,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


    }
}

