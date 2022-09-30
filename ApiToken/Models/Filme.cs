using Ioasys.Desafio.NET.Dominio.Models;
using System;
using System.Collections.Generic;

namespace ApiToken.Models
{
    public class Filme
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public string Diretor { get; set; }
        public string AtorId { get; set; }
        public bool Genero { get; set; }
        public string CriadoEm { get; private set; }
        public double Avaliacao { get; set; }
        public List<Ator> Ators { get; set; } = default;
        public List<FilmeAvaliacao> FilmeAvaliacao { get; set; } = default;

        public void Atualizar(DateTime dateTime)
        {
            CriadoEm = dateTime.ToString();
        }
    }
}

