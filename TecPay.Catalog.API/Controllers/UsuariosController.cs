using Azure.Core;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TecPay.Catalog.API._Clases;
using TecPay.Catalog.Application.Common;

namespace TecPay.Catalog.API.Controllers
{
    [ApiController, ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/Usuarios")]
    public sealed class UsuariosController(IConfiguration configuration) : ControllerBase
    {
        public sealed record LoginRequest(string Email, string Password);

        [HttpPost(nameof(Login))]
        [AllowAnonymous]
        public ActionResult<RespuestaAutenticacion> Login(LoginRequest request)
        {
            if (request.Email != "admin@tecpay.test" || request.Password != "Admin123!")
                return Unauthorized
                    (
                        new 
                        { 
                            message = "Login Incorrecto" 
                        }
                    );

            return GenerateToken(request.Email);
        }

        [HttpGet(nameof(RenewToken))]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult<RespuestaAutenticacion> RenewToken()
        {
            var email = "admin@tecpay.test";

            return GenerateToken(email);
        }

        private ActionResult<RespuestaAutenticacion> GenerateToken(string Email)
        {
            var jwt = configuration.GetSection("Jwt");
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, Email),
                new Claim(ClaimTypes.Role, "Admin")
            };

            var expiracion = DateTime.UtcNow.AddHours(8);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt["Key"]!));
            var token = new JwtSecurityToken(
                jwt["Issuer"],
                jwt["Audience"],
                claims,
                expires: expiracion,
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));



            UsuarioNETDTO usuarioNETDTO = new UsuarioNETDTO();
            usuarioNETDTO.Id = 1;
            usuarioNETDTO.Nombre = "Julio Cesar";
            usuarioNETDTO.Email = Email;
            usuarioNETDTO.Img = "3b7643a5-79a5-4121-9160-b7a244173ee8.jpg";
            usuarioNETDTO.Role = "Admin";
            usuarioNETDTO.Google = false;

            return new RespuestaAutenticacion()
            {
                Ok = true,
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Usuario = usuarioNETDTO,
                Expiracion = expiracion,
                Menu = MenuPersonalizado.GetListaMenu()
            };
        }
    }
}
