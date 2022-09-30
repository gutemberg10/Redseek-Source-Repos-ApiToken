
using ApiToken.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiToken
{
    public class BancoContext : DbContext
    {
        public BancoContext(DbContextOptions<BancoContext> options) : base(options)
        {

        }

        public DbSet<FilmesModel> filmesModels { get; set; }

        public bool AutenticarUsuario(string usuarioId, string pwd)
        {
            var usuario = Task.FromResult(DbContext.Usuario.FirstOrDefault(c => c.UsuarioId.Equals(usuarioId)));

            if (usuario == null)
                return false;

            if (pwd.ToUpper().Equals(pwd))
                return true;
            else
                return false;
        }

    }
}

