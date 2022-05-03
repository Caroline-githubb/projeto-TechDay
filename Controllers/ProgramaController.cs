using System.Net;
using System.Net.Mail;
using CarrefourApi.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CarrefourApi.Repository;

namespace CarrefourApi.Controllers;


[ApiController]
[Authorize]
[Route("[controller]")]
public class ProgramaController : ControllerBase
{
    private IProgramaRepository repository;
    private IInscricaoRepository repositoryEmail;
    
    public ProgramaController(IProgramaRepository repository, IInscricaoRepository repositoryEmail)
    {
        this.repository = repository;    
        this.repositoryEmail = repositoryEmail;    
    }
    
    [HttpGet]
    [AllowAnonymous]
    [Route("ListarProgramas")]
    public List<Programa> ListarProgramas()
    {        
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
        
        repository.CadastrarPrograma(programa);
        
        var emails = repositoryEmail.ListarEmails();

        //Config. envio de e-mail
        MailMessage message = new MailMessage(); //cria uma msg
        SmtpClient smtp = new SmtpClient(); //envia msg

        message.From = new MailAddress("ingrid.caroline.teste@gmail.com", "PROGRAMA TECH DAY CARREFOUR"); 

        foreach (var email in emails)
        {
            message.To.Add(new MailAddress(email.Email));
        }
        message.Subject = "NOVIDADE NO MUNDO DA TI PARA AS MULHERES"; //Assunto
        message.IsBodyHtml = false;
        message.Body = "Programa " + programa.Nome + ": " + programa.Tema +"\n" + "\n" + "Descrição: " + programa.Descricao + "\n Site: " + programa.Site;

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

        repository.AlterarPrograma(programa);

        return Ok();
    }

    [HttpDelete]
    [Route("deletarPrograma")]
    public void DeletarPrograma(int codigo)
    {
        repository.DeletarPrograma(codigo);
    }
}



