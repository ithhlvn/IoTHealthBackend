/********************************************************************************/
/* COPYRIGHT 										                            */
/* Authors reserve all rights even in the event of industrial property rights.  */
/* We reserve all rights of disposal such as copying and passing			    */
/* on to third parties. 										                */
/*													                            */
/* Description : Create EmrApi.Controllers                                      */
/*                                                                              */
/* Developers : LanHH, Vietnam                                                  */
/* -----------------------------------------------------------------------------*/
/* History 											                            */
/*													                            */
/* Started on : 06 May 2024							                            */
/* Revision : 1.0.0.0 									  	                    */
/* Changed by :     									                        */
/* Change date :                                                                */
/* Changes :                                                                    */
/* Reasons :   										                            */
/********************************************************************************/
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

using IOT.Models;
namespace IOT.Controllers
{
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IConfiguration _config;

        private readonly List<User> appUsers = new List<User>
        {
            new User {  FullName = "LanHH",  UserName = "admin", Password = "1", UserRole = "Admin" },
            new User {  FullName = "User",  UserName = "user", Password = "123", UserRole = "User" }
        };

        public LoginController(IConfiguration config)
        {
            _config = config;
        }

        /// <summary>
        /// Get the Bearer Token.
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [MapToApiVersion("2.0")]
        public IActionResult Login([FromBody] User login)
        {
            return LoginAs(login);
        }

        public IActionResult LoginAs(User login)
        {
            IActionResult response = Unauthorized();
            User user = AuthenticateUser(login);
            if (user != null)
            {
                var tokenString = GenerateJWTToken(user);
                response = Ok(new
                {
                    token = tokenString,
                    userDetails = user,
                });
            }
            return response;
        }

        private User AuthenticateUser(User loginCredentials)
        {
            User user = appUsers.SingleOrDefault(x => x.UserName == loginCredentials.UserName && x.Password == loginCredentials.Password);
            return user;
        }

        /// <summary>
        /// The GenerateJWTToken.
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        private string GenerateJWTToken(User userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:SecretKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userInfo.UserName),
                new Claim("fullName", userInfo.FullName.ToString()),
                new Claim("role",userInfo.UserRole),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddYears(10),
                signingCredentials: credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

    public static class JwtService
    {
        public static string _secretKey;

        public static string GenerateJwtToken(string username)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Convert.FromBase64String(_secretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                new Claim(ClaimTypes.Name, username)
                }),
                Expires = DateTime.UtcNow.AddYears(10), // Set expiry time to 10 years from now
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            _secretKey = tokenHandler.WriteToken(token);
            return tokenHandler.WriteToken(token);
        }
    }
}