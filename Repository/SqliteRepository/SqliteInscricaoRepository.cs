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
    }
}