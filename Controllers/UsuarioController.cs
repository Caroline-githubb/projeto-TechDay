using System.Data.SQLite;
using CarrefourApi.Model;
using CarrefourApi.Repository;
using CarrefourApi.Repository.SqliteRepository;
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
        IUsuarioRepository repository = new SqliteUsuarioRepository();

        if (repository.VerificarUsuario(email, senha))
        {
            return Ok(tokenService.GenerateToken(email));
        }

        return Unauthorized("usuário e senha incorretos");
    }


    [HttpPost]
    [Authorize]
    [Route("CadastrarUsuario")]
    public void CadastrarUsuario(Usuario usuario)
    {
        IUsuarioRepository repository = new SqliteUsuarioRepository();

        repository.CadastrarUsuario(usuario);
    }


}
