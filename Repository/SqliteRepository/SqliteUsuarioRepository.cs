using System.Data.SQLite;
using CarrefourApi.Model;

namespace CarrefourApi.Repository.SqliteRepository
{
    public class SqliteUsuarioRepository : IUsuarioRepository
    {
        public void CadastrarUsuario(Usuario usuario)
        {
            using (var connection = new SQLiteConnection("Data Source=bancocarrefour.db"))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText =
                @$"
                    INSERT INTO Usuario (Nome, Email, Senha)
                    VALUES('{usuario.Nome}', '{usuario.Email}', '{usuario.Senha}')
                ";
                // command.Parameters.AddWithValue("$id", id);

                command.ExecuteNonQuery();
            }
        }

        public bool VerificarUsuario(string email, string senha)
        {
            using (var connection = new SQLiteConnection("Data Source=bancocarrefour.db"))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText =
                @$"
                    SELECT *
                    FROM Usuario
                    WHERE Email = '{email}' AND Senha = '{senha}'
                ";
                //command.Parameters.AddWithValue("$id", id);

                using (var reader = command.ExecuteReader())
                {
                    return reader.Read();
                }

                
            }
        }
    }
}