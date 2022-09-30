using ApiToken.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiToken
{
    public class FilmeService : IFilmeService
    {
        private readonly BancoContext bancoContext;

        public FilmeService(BancoContext bancoContext)
        {
            this.bancoContext = bancoContext;
        }

        public async Task<Filme> BuscarByIdAsync(int filmeID)
        {
            var filme = bancoContext.Filmes.Find(x => x.Id.Equals(filmeID));
            return await Task.FromResult(filme).ConfigureAwait(false);
        }

        public async Task<List<Filme>> BuscarTodosFilmes(CancellationToken cancellationToken)
        {
            var filmes = bancoContext.Filmes.ToList();

            return await Task.FromResult(filmes).ConfigureAwait(false);
        }

        public async Task<List<FilmeViewModel>> BuscarTodosFilmes(FilmeFiltroViewModel filmeFiltroViewModel)
        {
            var filmes = bancoContext.Filmes.AsQueryable();

            if (string.IsNullOrEmpty(filmeFiltroViewModel.Ator))
                filmes.Where(c => c.AtorId.Equals(filmeFiltroViewModel.Ator));

            if (string.IsNullOrEmpty(filmeFiltroViewModel.NomeDoFilme))
                filmes.Where(c => c.Nome.Equals(filmeFiltroViewModel.NomeDoFilme));

            var result = filmes.Select(c => new FilmeViewModel
            {
                Id = c.Id,
                NomeDoFilme = c.Nome,
                Ator = c.Ators.Select(c => c.Nome).ToArray(),
                Diretor = c.Diretor,
                Genero = c.Genero
            }).ToList();

            return await Task.FromResult(result).ConfigureAwait(false);
        }

        public File AdicionarFilme(FilmeViewModel filmeViewModel)
        {
            var ators = bancoContext.Ator.Where(c => c.Nome.Equals(filmeViewModel.Ator));
            var diretor = bancoContext.Diretor.Find(c => c.Nome.Equals(filmeViewModel.Diretor));

            //Verificar se existe ator
            if (ators == null)
                throw new InvalidOperationException("Ator solicitada não foi idetificado para o registro do filme.");

            //Verificar se existe diretor
            if (diretor is null)
                throw new InvalidOperationException("Diretor solicitado não foi idetificado para o registro do filme.");

            var novoFilme = new Filme
            {
                Id = filmeViewModel.Id,
                Nome = filmeViewModel.NomeDoFilme,
                Ators = ators.ToList(),
                Diretor = filmeViewModel.Diretor,
                Genero = filmeViewModel.Genero,
            };
            novoFilme.Atualizar(DateTime.Now);
            bancoContext.Filmes.Add(novoFilme);
            return novoFilme;
        }

        public async Task VotarFilme(FilmeAvaliarViewModel filmeAvaliarViewModel)
        {
            var votoMaximo = 4;
            var votoMinimo = 0;

            if (votoMinimo < 0)
                throw new InvalidOperationException("Não é possivel avaliar o filme com nota menor que zero.");

            if (votoMaximo > 5)
                throw new InvalidOperationException("Não é possivel avaliar o filme com nota maior que quatro.");

            var usuario = bancoContext.Usuario.Find(c => c.UsuarioId.ToString().Equals(filmeAvaliarViewModel.Usuario));

            if (usuario.Ativo == false)
                throw new InvalidOperationException("Não é possivel avaliar o filme pois o usuário está inativo.");

            var filmes = bancoContext.Filmes.Find(c => c.Id.Equals(filmeAvaliarViewModel.FilmeId));

            if (filmes is null)
                throw new InvalidOperationException("Filme solicitado não foi idetificado no banco de dados");

            if (!AutenticarUsuario(filmeAvaliarViewModel.Usuario, filmeAvaliarViewModel.Pwd))
                throw new InvalidOperationException("Não é possivel avaliar o filme pois o usuário não está autenticado.");

            filmes.FilmeAvaliacao.Add(new FilmeAvaliacao { FilmeId = Convert.ToInt32(filmes.Id), Usuario = filmeAvaliarViewModel.Usuario, Voto = filmeAvaliarViewModel.QuantidadeDeVotos });
            filmes.Avaliacao = filmes.FilmeAvaliacao.Average(c => c.Voto);
            await bancoContext.SaveChanges();
            await Task.CompletedTask;
        }

        protected bool AutenticarUsuario(string usuarioId, string pwd)
        {
            var usuario = Task.FromResult(bancoContext.Usuario.FirstOrDefault(c => c.UsuarioId.Equals(usuarioId)));

            if (usuario == null)
                return false;

            if (pwd.ToUpper().Equals(pwd))
                return true;
            else
                return false;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

    }
}

