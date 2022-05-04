using System.Net;
using System.Net.Mail;
using CarrefourApi.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CarrefourApi.Repository;
using CarrefourApi.Service;

namespace CarrefourApi.Controllers;


[ApiController]
[Authorize]
[Route("[controller]")]
public class ProgramaController : ControllerBase
{
    private IProgramaRepository repository;
    private IInscricaoRepository repositoryEmail;

    private EmailService emailService;

    public ProgramaController(IProgramaRepository repository, IInscricaoRepository repositoryEmail, EmailService emailService)
    {
        this.repository = repository;
        this.repositoryEmail = repositoryEmail;
        this.emailService = emailService;
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

        try
        {
            var emails = repositoryEmail.ListarEmails();

            this.emailService.EnviarEmail(programa, emails);

            return Ok();
        } catch {
            return Ok("Falhou envio de email");
        }
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



