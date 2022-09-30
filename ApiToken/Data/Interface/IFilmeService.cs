
using ApiToken.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ApiToken
{
    public interface IFilmeService : IDisposable
    {
        Task<List<Filme>> BuscarTodosFilmes(CancellationToken cancellationToken);
        Task<List<FilmeViewModel>> BuscarTodosFilmes(FilmeFiltroViewModel filmeFiltroViewModel);
        Task<Filme> BuscarByIdAsync(int filmeID);
        Filme AdicionarFilme(FilmeViewModel filmeViewModel);
        Task VotarFilme(FilmeAvaliarViewModel filmeAvaliarViewModel);
    }
}

