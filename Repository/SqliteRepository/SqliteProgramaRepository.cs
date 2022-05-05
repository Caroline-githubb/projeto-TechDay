using System.Data.SQLite;
using CarrefourApi.Model;

namespace CarrefourApi.Repository.SqliteRepository
{
    public class SqliteProgramaRepository : IProgramaRepository
    {
        public void AlterarPrograma(Programa programa)
        {
            using (var connection = new SQLiteConnection("Data Source=bancocarrefour.db"))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText =
                @$"
            UPDATE
	            Programa
            SET
                Nome  = '{programa.Nome}',
	            Tema = '{programa.Tema}',
                Descricao = '{programa.Descricao}',
                Site = '{programa.Site}'                
            WHERE
	            Codigo = {programa.Codigo}                  
           
            ";
                //command.Parameters.AddWithValue("$id", id);
                command.ExecuteNonQuery();
            }

        }

        public void CadastrarPrograma(Programa programa)
        {
            using (var connection = new SQLiteConnection("Data Source=bancocarrefour.db"))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText =
                @$"
                INSERT INTO Programa (Nome, Tema, Descricao, Site)
                VALUES('{programa.Nome}', '{programa.Tema}', '{programa.Descricao}', '{programa.Site}')
            ";
                // command.Parameters.AddWithValue("$id", id);            
                command.ExecuteNonQuery();
            }
        }

        public void DeletarPrograma(int codigo)
        {
            using (var connection = new SQLiteConnection("Data Source=bancocarrefour.db"))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText =
                @$"

            DELETE FROM Programa WHERE Codigo = {codigo} 
           
            ";
                //command.Parameters.AddWithValue("$id", id);
                command.ExecuteNonQuery();
            }
        }

        public List<Programa> ListarProgramas()
        {
            using (var connection = new SQLiteConnection("Data Source=bancocarrefour.db"))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText =
                @$"
                SELECT Codigo, Nome, Tema, Descricao, Site
                FROM Programa
            ";

                List<Programa> retorno = new List<Programa>();
                using (var reader = command.ExecuteReader()) //executar leitor no sqlite
                {
                    while (reader.Read()) //Read -> lê linha por linha do db
                    {
                        Programa programa = new Programa();

                        programa.Codigo = reader.GetInt32(0);
                        programa.Nome = (string)reader["Nome"];
                        programa.Tema = (string)reader["Tema"];
                        programa.Descricao = (string)reader["Descricao"];
                        programa.Site = (string)reader["Site"];

                        retorno.Add(programa); //Adiciona os itens acima na lista
                    }
                }
                return retorno;
            }
        }
    }
}