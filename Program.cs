using System.Text;
using Microsoft.Data.SqlClient;
using Models;

Console.WriteLine("Programa Teste - Exercicio01");

Console.WriteLine("\nLista de Pessoas:");
Console.WriteLine("=========================================\n");
var ListPessoa = new List<Pessoa>() {
            new Pessoa {Id = 1, Nome = "João Paulo", DataDeCriacao = DateTime.Now},
            new Pessoa {Id = 2, Nome = "Fenando Silva", DataDeCriacao = DateTime.Now},
            new Pessoa {Id = 3, Nome = "Maria Oliveira", DataDeCriacao = DateTime.Now},
            new Pessoa {Id = 4, Nome = "José Osias", DataDeCriacao = DateTime.Now},
            new Pessoa {Id = 5, Nome = "Sandro Fausto", DataDeCriacao = DateTime.Now},
            new Pessoa {Id = 6, Nome = "Maria Paula", DataDeCriacao = DateTime.Now},
            new Pessoa {Id = 7, Nome = "Silva Pereira", DataDeCriacao = DateTime.Now},
            new Pessoa {Id = 8, Nome = "Roberta Cruz", DataDeCriacao = DateTime.Now},
            new Pessoa {Id = 9, Nome = "Susana Matiz", DataDeCriacao = DateTime.Now},
            new Pessoa {Id = 10, Nome = "Alessandra Diniz", DataDeCriacao = DateTime.Now}
        };

foreach (var pessoa in ListPessoa)
{
    Console.WriteLine($"Id: {pessoa.Id}, Nome: {pessoa.Nome}, DataDeCriacao: {pessoa.DataDeCriacao}");
}

try
{
    // Build connection string
    Console.WriteLine("\n\nConectando-se ao banco SQL Server...");
    SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

    // conexão
    builder.ConnectionString = "Server=DESKTOP-E58P335\\SQLTREINAMENTO;Encrypt=False;Trusted_Connection=True;";
    

    // Connect to SQL
    using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
    {
        connection.Open();
        Console.WriteLine("Conexão aberta.");

        // Criando database
        String sql = "DROP DATABASE IF EXISTS [TesteDB]; CREATE DATABASE [TesteDB]";
        using (SqlCommand command = new SqlCommand(sql, connection))
        {
            command.ExecuteNonQuery();
            Console.WriteLine("Banco de dados 'TesteDB' criado.");
        }

        // Criando tabela 
        StringBuilder sb = new StringBuilder();
        sb.Append("USE TesteDB; ");
        sb.Append("CREATE TABLE Pessoas ( ");
        sb.Append(" Id INT NOT NULL, ");
        sb.Append(" Nome NVARCHAR(100), ");
        sb.Append(" DataDeCriacao datetime ");
        sb.Append("); ");
        sql = sb.ToString();
        using (SqlCommand command = new SqlCommand(sql, connection))
        {
            command.ExecuteNonQuery();
            Console.WriteLine("Tabela Pessoas criada.\n\n");
        }

        // Inserido lista de pessoas
        sb.Clear();
        Console.WriteLine("Iniciando processo de inclusão de lista de pessoas....\n");
        sb.Append("INSERT Pessoas (Id, Nome, DataDeCriacao) ");
        sb.Append("VALUES (@id, @nome, @datacriacao);");
        sql = sb.ToString();
        using (SqlCommand command = new SqlCommand(sql, connection))
        {
            // Loop 
            foreach (var pessoa in ListPessoa)
            {
                Console.WriteLine($"Id: {pessoa.Id}, Nome: {pessoa.Nome}, DataDeCriacao: {pessoa.DataDeCriacao}");
                command.Parameters.AddWithValue("@id", $"{pessoa.Id}");
                command.Parameters.AddWithValue("@nome", $"{pessoa.Nome}");
                command.Parameters.AddWithValue("@datacriacao", $"{pessoa.DataDeCriacao}");
                int rowsAffected = command.ExecuteNonQuery();
                command.Parameters.Clear();                
                Console.WriteLine("row inserida.\n");
            }
        }
        connection.Close();
        Console.WriteLine("\n\nProcesso concluído!\nPressione alguma tecla para desligar o programa...");
        Console.ReadKey(true);

    }
}
catch (SqlException e)
{
    Console.WriteLine(e.ToString());
}





