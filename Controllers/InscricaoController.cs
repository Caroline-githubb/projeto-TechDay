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
    // o tipo de retorno de uma action.
    // Actions podem retornar diversas coisas, como: models para views, file stream, redirects, javascript, etc.
    //Os tipos ActionResult representam vários códigos de status HTTP. Alguns tipos de retorno comuns nessa categoria
    // são BadRequestResult (400), NotFoundResult (404) e OkObjectResult (200).
    {

        if (string.IsNullOrWhiteSpace(inscricao.Nome)) //indica se o parametro é vazio ou contem espaços em branco
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