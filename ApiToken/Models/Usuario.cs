using System;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiToken
{
    public class Usuario
    {
        public int UsuarioId { get; set; }
        public string Nome { get; set; }
        public string Senha { get; set; }
        public string Descriçao { get; set; }
        public int Idade { get; set; }
        public bool Ativo { get; set; }
        public DateTime CriadoEm { get; set; }
    }
}

