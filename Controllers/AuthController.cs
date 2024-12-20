using IOT.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using IOT.Services;
using System.Linq;
using System.Configuration;

namespace IOT.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly JwtService _jwtService;

        public AuthController(JwtService jwtService)
        {
            _jwtService = jwtService; // Inject JwtService thông qua Dependency Injection

        }

        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] User model)
        {
            try
            {
                // Kiểm tra thông tin đăng nhập (giả lập)
                if (model.Account == "admin" && model.Password == "123456")
                {
                    var user = new User
                    {
                        UserId = 1,
                        Account = "admin",
                        Role = "Administrator"
                    };

                    // Sinh token
                    var token = _jwtService.GenerateToken(user);

                    return Ok(new { token });
                }
            }
            catch (Exception ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            return Unauthorized(new { message = "Sai tên đăng nhập hoặc mật khẩu!" });
        }

        [HttpPost("validate-token")]
        public IActionResult ValidateToken([FromHeader] string authorization)
        {
            if (string.IsNullOrEmpty(authorization) || !authorization.StartsWith("Bearer "))
            {
                return BadRequest(new { message = "Token không hợp lệ hoặc không được cung cấp." });
            }

            var token = authorization.Substring(7); // Bỏ tiền tố "Bearer "

            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = _configuration["Jwt:Issuer"],
                    ValidAudience = _configuration["Jwt:Audience"],
                    ValidateLifetime = true // Kiểm tra thời gian hết hạn của token
                }, out SecurityToken validatedToken);

                // Token hợp lệ, có thể lấy thông tin từ claims
                var jwtToken = (JwtSecurityToken)validatedToken;
                var username = jwtToken.Claims.First(x => x.Type == JwtRegisteredClaimNames.Sub).Value;

                return Ok(new { message = "Token hợp lệ", username });
            }
            catch (Exception ex)
            {
                return Unauthorized(new { message = "Token không hợp lệ", error = ex.Message });
            }
        }
    }
}
