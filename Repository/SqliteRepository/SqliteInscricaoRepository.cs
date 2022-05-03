using System.Data.SQLite;
using CarrefourApi.Model;

namespace CarrefourApi.Repository.SqliteRepository
{
    public class SqliteInscricaoRepository : IInscricaoRepository
    {


        public void InserirInscricao(Inscricao inscricao)
        {
            using (var connection = new SQLiteConnection("Data Source=bancocarrefour.db"))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText =
                @$"
                INSERT INTO Inscricao (Nome, Email)
                VALUES('{inscricao.Nome}', '{inscricao.Email}')
            ";
                // command.Parameters.AddWithValue("$id", id);

                command.ExecuteNonQuery();
            }
        }

        public List<Inscricao> ListarEmails()
        {
            using (var connection = new SQLiteConnection("Data Source=bancocarrefour.db"))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText =
                @$"
               SELECT Email
               FROM Inscricao
            ";
                // command.Parameters.AddWithValue("$id", id);            
                command.ExecuteNonQuery();


                List<Inscricao> emails = new List<Inscricao>();
                using (var reader = command.ExecuteReader()) //executar leitor no sqlite
                {
                    while (reader.Read()) //Read -> lê linha por linha do db
                    {
                        Inscricao email = new Inscricao();
                        email.Email = (string)reader["Email"];
                        emails.Add(email);                        
                    }
                }         
                return emails;                            
                            
            }
        }
    }
}