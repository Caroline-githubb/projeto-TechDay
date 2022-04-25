using System.Data.SQLite;
using CarrefourApi.Model;
using Microsoft.AspNetCore.Mvc;

namespace CarrefourApi.Controllers;

[ApiController]
[Route("[controller]")]
public class InscricaoController : ControllerBase
{
    [HttpPost]
    [Route("InscricaoEmail")]
    public ActionResult InscricaoEmail(Inscricao inscricao)
    {

        if (string.IsNullOrWhiteSpace(inscricao.Nome)) {
            return BadRequest("O campo nome é obrigatorio");
        }

        if (string.IsNullOrWhiteSpace(inscricao.Email)) {
            return BadRequest("O campo e-mail é obrigatorio");
        }

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

        return Ok();
    }
}