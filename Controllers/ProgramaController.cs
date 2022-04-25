using System.Data.SQLite;
using System.Net;
using System.Net.Mail;
using CarrefourApi.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarrefourApi.Controllers;


[ApiController]
[Authorize]
[Route("[controller]")]
public class ProgramaController : ControllerBase
{

    [HttpGet]
    [AllowAnonymous]
    [Route("ListarProgramas")]
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

    [HttpPost]
    [Route("CadastrarPrograma")]
    public ActionResult CadastrarPrograma(Programa programa)
    {
        if (string.IsNullOrWhiteSpace(programa.Nome)) {
            return BadRequest("O campo nome é obrigatorio");
        }

        if (string.IsNullOrWhiteSpace(programa.Tema)) {
            return BadRequest("O campo tema é obrigatorio");
        }

        if (string.IsNullOrWhiteSpace(programa.Site)) {
            return BadRequest("O campo site é obrigatorio");
        }

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

            command.CommandText =
            @$"
                SELECT Email
                FROM Inscricao
            ";

            List<string> emails = new List<string>();
            using (var reader = command.ExecuteReader()) //executar leitor no sqlite
            {
                while (reader.Read()) //Read -> lê linha por linha do db
                {                    
                    emails.Add((string)reader["Email"]); //Adiciona os itens acima na lista
                }
            }
            
            MailMessage message = new MailMessage();
            SmtpClient smtp = new SmtpClient();

            message.From = new MailAddress("ingrid.caroline.teste@gmail.com", "PROGRAMA TECH DAY CARREFOUR");

            foreach(string email in emails)
            {
                message.To.Add(new MailAddress(email));
            }            
            message.Subject = "NOVIDADE NO MUNDO DA TI PARA AS MULHERES";
            message.IsBodyHtml = false;
            message.Body = "Entre no site ou consulte a API para saber mais infomações do programa " + programa.Nome;

            smtp.Port = 587;
            smtp.Host = "smtp.gmail.com";
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential("ingrid.caroline.teste@gmail.com", "gdheeoctdoogcqwo");
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Send(message);
        }

        return Ok();
    }

    [HttpPut]
    [Route("alterarPrograma")]
    public ActionResult alterarPrograma(Programa programa)
    {
        if (string.IsNullOrWhiteSpace(programa.Nome)) {
            return BadRequest("O campo nome é obrigatorio");
        }

        if (string.IsNullOrWhiteSpace(programa.Tema)) {
            return BadRequest("O campo tema é obrigatorio");
        }

        if (string.IsNullOrWhiteSpace(programa.Site)) {
            return BadRequest("O campo site é obrigatorio");
        }

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

        return Ok();
    }

    [HttpDelete]
    [Route("deletarPrograma")]
    public void deletarPrograma(int codigo)
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
}



