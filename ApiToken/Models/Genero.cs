namespace ApiToken
{
    public class Genero
    {
        public short Id { get; private set; }
        public string Nome { get; private set; }
        public string Descricao { get; private set; }
        public Genero(short id, string nome, string descricao)
        {
            Id = id;
            Nome = nome;
            Descricao = descricao;
        }
    }
}

