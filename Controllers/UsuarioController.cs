using System.Data.SQLite;
using CarrefourApi.Model;
using CarrefourApi.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarrefourApi.Controllers;

[ApiController]
[Route("[controller]")]
public class UsuarioController : ControllerBase
{

    private TokenService tokenService;

    public UsuarioController(TokenService tokenService)
    {
        this.tokenService = tokenService;
    }

    [HttpGet]
    [Route("VerificarUsuario")]
    public ActionResult<string> VerificarUsuario(string email, string senha)
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
                if (reader.Read()) {
                    return Ok(tokenService.GenerateToken(email));
                }
            }

            return Unauthorized("usuário e senha incorretos");
        }
    }

    
    [HttpPost]
    [Authorize] //somente um usuário especifico pode cadastrar outro usuário
    [Route("CadastrarUsuario")]
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


}
