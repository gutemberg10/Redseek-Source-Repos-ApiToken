
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiToken
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmeController 
    {
        private readonly IFilmeService _filmeService;

        public FilmeController(IFilmeService filmeService)
        {
            _filmeService = filmeService;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Getasync(CancellationToken cancellationToken)
        {
            try
            {

                var filmes = await _filmeService.BuscarTodosFilmes(cancellationToken);

                if (filmes == null)
                    return NoContent();

                Console.WriteLine($"Retornou: {filmes.Count} registros");

                return Ok(filmes);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex });
            }
        }

        [HttpGet]
        [Route("get-buscar-com-filtro")]
        public IActionResult GetBusca(FilmeFiltroViewModel filmeFiltroViewModel)
        {
            try
            {
                var filmes = _filmeService.BuscarTodosFilmes(filmeFiltroViewModel).Result;

                return Ok(filmes);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex });
            }
        }

        [HttpGet]
        [Route("filme/{idfilme}")]
        public async Task<IActionResult> GetPorId(int idFilme)
        {
            try
            {
                var filmes = _filmeService.BuscarByIdAsync(idFilme).Result;

                if (filmes == null)
                    return NoContent();

                return Ok(filmes);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex });
            }
        }

        [HttpPost]
        [Route("adicionar")]
        public async Task<IActionResult> PostAdicionarFilmeAsync(FilmeViewModel filmeViewModel)
        {
            try
            {
                var valid = AutenticarUsuario(filmeViewModel.Usuario, filmeViewModel.Pwd);
                if (!valid)
                    return Unauthorized();

                var filmes = _filmeService.AdicionarFilme(filmeViewModel);

                if (filmes == null)
                    return NotFound();

                return Ok(filmes);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex });
            }
        }

        [HttpPost]
        [Route("avaliarFilme")]
        public async Task<IActionResult> PutAvaliarFilme(FilmeAvaliarViewModel filmeAvaliarViewModel)
        {
            try
            {
                var usuarioValido = AutenticarUsuario(filmeAvaliarViewModel.Usuario, filmeAvaliarViewModel.Pwd);
                if (!usuarioValido)
                    return Unauthorized();

                var filmes = _filmeService.VotarFilme(filmeAvaliarViewModel);

                if (filmes == null)
                    return NotFound();

                return Ok(filmes);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex });
            }
        }
    }
}

