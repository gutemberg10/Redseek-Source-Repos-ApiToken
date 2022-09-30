
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ApiToken
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly BancoContext _bancoContext;

        public UsuarioController(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;
        }

        [HttpGet]
        [Route("/get-usuarios")]
        public async Task<IActionResult> GetBuscarTodosUsuarios()
        {
            try
            {
                var usuarios = _bancoContext.Usuario.ToList();

                if (usuarios == null)
                    return NoContent();

                return Ok(usuarios);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex });
            }
        }

        [HttpGet]
        [Route("{idUsuario}")]
        public async Task<IActionResult> GetById([FromRoute] string idUsuario)
        {
            try
            {
                if (string.IsNullOrEmpty(idUsuario))
                    throw new ArgumentNullException("Não foi possível identificar o usuário.");

                var usuario = await Task.FromResult(_bancoContext.Usuario.Where(c => c.UsuarioId.Equals(idUsuario)));

                if (usuario == null)
                    return NoContent();

                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex });
            }
        }

        [HttpPost]
        [Route("autenticar")]
        public async Task<IActionResult> Autenticar([FromRoute] string idUsuario, string pwd)
        {
            try
            {
                var usuario = await Task.FromResult(_bancoContext.Usuario.FirstOrDefault(c => c.UsuarioId.Equals(idUsuario)));

                if (usuario == null)
                    return NoContent();

                if (pwd.ToUpper().Equals(usuario.Senha))
                    return Ok(usuario);
                else
                    return Unauthorized();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex });
            }
        }
    }
}

