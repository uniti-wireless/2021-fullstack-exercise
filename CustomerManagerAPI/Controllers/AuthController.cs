using CustomerManagerAPI.Configuration;
using CustomerManagerAPI.Data;
using CustomerManagerAPI.Helpers;
using CustomerManagerAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CustomerManagerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        // Jwt settings for secret key
        private readonly JwtSettings jwtSettings;

        // Login user data
        public UserDBContext userContext;

        public AuthController(IOptionsMonitor<JwtSettings> optionsMonitor)
        {
            userContext = new UserDBContext();
            jwtSettings = optionsMonitor.CurrentValue;
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login([FromBody] LoginModel user)
        {
            if (ModelState.IsValid && user != null)
            {
                // Get user with the provided user name
                LoginModel existingUser = userContext.Users.FirstOrDefault(x => x.UserName == user.UserName);

                if (existingUser == null)
                {
                    // Error message
                    return BadRequest(new AuthenticationResult()
                    {
                        ErrorMessage = "Invalid UserName!",
                        LoginSuccess = false
                    }); ;
                }

                // Cmpare provided password with the actual user password
                LoginModel validUser = userContext.Users.FirstOrDefault(x => x.UserName == user.UserName && x.Password == user.Password);

                if (validUser == null)
                {
                    // Error message
                    return BadRequest(new AuthenticationResult()
                    {
                        ErrorMessage = "Invalid Password!",
                        LoginSuccess = false
                    });
                }

                // Generate JWT token
                string jwtToken = GenerateJwtToken(existingUser);
                AuthenticationResult authenticationResult = new AuthenticationResult()
                {
                    Token = jwtToken,
                    LoginSuccess = true
                };

                Response.Headers.Add("AuthResult", JsonConvert.SerializeObject(authenticationResult));

                return Ok(authenticationResult);
            }

            // Error message
            return BadRequest(new AuthenticationResult()
            {
                ErrorMessage = "Invalid Model State or user!",
                LoginSuccess = false
            });
        }

        #region JWT Token method
        // This method genrates the JWT token from the provided secret key after the valid login
        private string GenerateJwtToken(LoginModel user)
        {
            // Intialize JWT Security token handler
            JwtSecurityTokenHandler jwtTokenHandler = new JwtSecurityTokenHandler();

            // Encode secret key to bytes
            byte[] key = Encoding.ASCII.GetBytes(jwtSettings.Secret);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("UserName", user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(6),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            // Create security token
            SecurityToken token = jwtTokenHandler.CreateToken(tokenDescriptor);
            // Write security tokrn to string
            string jwtToken = jwtTokenHandler.WriteToken(token);

            return jwtToken;
        }
        #endregion
    }
}
