using CarrefourApi.Model;
using CarrefourApi.Repository;
using CarrefourApi.Repository.SqliteRepository;
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

        IInscricaoRepository repository = new SqliteInscricaoRepository();
        repository.InserirInscricao(inscricao);

        return Ok();
    }
}