using ApiToken.Models;
using Bogus;
using Ioasys.Desafio.NET.Dominio.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiToken
{
    /// <summary>
    /// Camada com dados Fake
    /// </summary>
    public class DbContext
    {
        public List<Filme> Filmes { get; set; } = new List<Filme>();
        public List<Usuario> Usuario { get; set; } = new List<Usuario>();
        public List<Ator> Ator { get; set; } = new List<Ator>();
        public List<Diretor> Diretor { get; set; } = new List<Diretor>();
        public DbContext(Microsoft.EntityFrameworkCore.DbContextOptions<BancoContext> options) => LoadFakeData();
        public async Task SaveChanges() => await Task.CompletedTask;

        private void LoadFakeData()
        {
            Filmes = new Faker<Filme>()
                .RuleFor(s => s.Id, f => f.UniqueIndex.ToString())
                .RuleFor(s => s.Nome, f => f.Name.FullName())
                .RuleFor(s => s.Diretor, f => f.Name.FullName())
                .RuleFor(s => s.AtorId, f => f.Name.FullName())
                .RuleFor(s => s.CriadoEm, f => f.Date.Past().Date.ToString())
                .Generate(10)
                .ToList();

            Usuario = new Faker<Usuario>()
                .RuleFor(s => s.UsuarioId, f => f.UniqueIndex)
                .RuleFor(s => s.Nome, f => f.Name.FullName())
                .RuleFor(s => s.Senha, f => f.Random.Guid().ToString())
                .RuleFor(s => s.Descriçao, f => f.Commerce.ProductName())
                .RuleFor(s => s.Idade, 20)
                .RuleFor(s => s.CriadoEm, f => f.Date.Future().Date)
                .Generate(5)
                .ToList();

            Ator = new Faker<Ator>()
               .RuleFor(s => s.Id, f => f.Random.Guid())
               .RuleFor(s => s.Nome, f => f.Name.FullName())
               .RuleFor(s => s.Sexo, f => f.Person.Gender.ToString())
               .RuleFor(s => s.Idade, 20)
               .Generate(20)
               .ToList();

            Diretor = new Faker<Diretor>()
              .RuleFor(s => s.Id, f => f.Random.Guid())
              .RuleFor(s => s.Nome, f => f.Name.FullName())
              .RuleFor(s => s.Sexo, f => f.Person.Gender.ToString())
              .RuleFor(s => s.Idade, 20)
              .Generate(20)
              .ToList();
        }
    }
}

