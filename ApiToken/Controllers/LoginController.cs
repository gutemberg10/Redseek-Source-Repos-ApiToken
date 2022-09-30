using ApiToken.Models;
using ApiToken.Repositories;
using ApiToken.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiToken.Controllers
{
    [ApiController]
    [Route("v1")]
    public class LoginController : ControllerBase
    {
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<dynamic>> AuthenticateAsync([FromBody] User model)
        {
            //Recupar o usuário
            var user = UserRepository.Get(model.UserName, model.Password);
            //Verifica se o usuário existe
            if (user == null)
     
                return NotFound(new { messege = "Usuário ou senhas inválidos" });

                // Gera o Token
                var token = TokenService.GenerateToken(user);

                // Oculta a senha
                user.Password = "";

                // Retorna os dados
                return new
                {
                    user = user,
                    token = token
                };
            
        }
    }
}
