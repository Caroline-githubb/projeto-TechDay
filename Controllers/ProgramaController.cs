using System.Data.SQLite;
using System.Net;
using System.Net.Mail;
using CarrefourApi.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CarrefourApi.Repository;
using CarrefourApi.Repository.SqliteRepository;

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
        IProgramaRepository repository = new SqliteProgramaRepository();
        return repository.ListarProgramas();

    }

    [HttpPost]
    [Route("CadastrarPrograma")]
    public ActionResult CadastrarPrograma(Programa programa)
    {
        if (string.IsNullOrWhiteSpace(programa.Nome))
        {
            return BadRequest("O campo nome é obrigatorio");
        }

        if (string.IsNullOrWhiteSpace(programa.Tema))
        {
            return BadRequest("O campo tema é obrigatorio");
        }

        if (string.IsNullOrWhiteSpace(programa.Site))
        {
            return BadRequest("O campo site é obrigatorio");

        }

        IProgramaRepository repository = new SqliteProgramaRepository();
        repository.CadastrarPrograma(programa);

        IInscricaoRepository emailRepository = new SqliteInscricaoRepository();
        var emails = emailRepository.ListarEmails();


        MailMessage message = new MailMessage();
        SmtpClient smtp = new SmtpClient();

        message.From = new MailAddress("ingrid.caroline.teste@gmail.com", "PROGRAMA TECH DAY CARREFOUR");

        foreach (var email in emails)
        {
            message.To.Add(new MailAddress(email.Email));
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


        return Ok();
    }

    [HttpPut]
    [Route("alterarPrograma")]
    public ActionResult AlterarPrograma(Programa programa)
    {
        if (string.IsNullOrWhiteSpace(programa.Nome))
        {
            return BadRequest("O campo nome é obrigatorio");
        }

        if (string.IsNullOrWhiteSpace(programa.Tema))
        {
            return BadRequest("O campo tema é obrigatorio");
        }

        if (string.IsNullOrWhiteSpace(programa.Site))
        {
            return BadRequest("O campo site é obrigatorio");
        }

        IProgramaRepository repository = new SqliteProgramaRepository();
        repository.AlterarPrograma(programa);

        return Ok();
    }

    [HttpDelete]
    [Route("deletarPrograma")]
    public void DeletarPrograma(int codigo)
    {
        IProgramaRepository repository = new SqliteProgramaRepository();
        repository.DeletarPrograma(codigo);
    }
}



