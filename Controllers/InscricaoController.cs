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
    public ActionResult InscricaoEmail(Inscricao inscricao) //Um action result é o tipo de retorno de um método,
    
    {

        if (string.IsNullOrWhiteSpace(inscricao.Nome))
        {
            return BadRequest("O campo nome é obrigatorio"); //O código de status de resposta HTTP 400 Bad Request indica 
            //que o servidor não pode ou não irá processar a requisição devido a alguma coisa que foi entendida como um erro
            // do cliente
        }

        if (string.IsNullOrWhiteSpace(inscricao.Email)) 
        {
            return BadRequest("O campo e-mail é obrigatorio");
        }

        using (var connection = new SQLiteConnection("Data Source=bancocarrefour.db")) //Inicializa a conexão com o db
        {
            connection.Open(); //abre a conexão usando os paramentros encontrados no db 

            var command = connection.CreateCommand();//manipulação do db
            command.CommandText =
            @$"
                INSERT INTO Inscricao (Nome, Email)
                VALUES('{inscricao.Nome}', '{inscricao.Email}')
            ";
            // command.Parameters.AddWithValue("$id", id);

            command.ExecuteNonQuery();//executa o db 
        }

        return Ok();
    }
}