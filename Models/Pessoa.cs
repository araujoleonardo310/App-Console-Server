
namespace Models
{
    public class Pessoa
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataDeCriacao { get; set; }

        public Pessoa() {

        }

        public Pessoa(int id, string nome, DateTime datacriacao )
        {
           Id = id;
           Nome = nome;
           DataDeCriacao = datacriacao;
        }
    }
}