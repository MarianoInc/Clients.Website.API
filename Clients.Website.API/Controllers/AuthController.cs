using Domain.ViewModels;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Clients.Website.API.Controllers
{
    [Route("api/v1/[controller]")]
    [EnableCors("ClientsWebSiteCorsPolicy")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AuthController(IConfiguration config)
        {
            _configuration = config;
        }

        [HttpPost]
        [Route("/login")]
        public IActionResult Login([FromBody] Login request)
        {
            if (ModelState.IsValid && request.UserName.Equals("admin") && request.Password.Equals("1234"))
            {
                var token = GenerateToken(request.UserName);

                return Ok(new WebApiResponse<string> { IsSuccess = true, Message = $"Bienvenido {request.UserName}", Value = token!});
            }
            

            return Unauthorized(new WebApiResponse<string> { IsSuccess = false, Message = "No pudimos validar tu identidad" });
        }

        private string GenerateToken(string username)
        {
            var jwtSettings = _configuration.GetSection("Jwt");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(int.Parse(jwtSettings["ExpiresInMinutes"]!)),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        
    }
}
