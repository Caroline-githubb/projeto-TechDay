using CarrefourApi.Model;
using CarrefourApi.Repository;
using CarrefourApi.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarrefourApi.Controllers;

[ApiController]
[Route("[controller]")]
public class UsuarioController : ControllerBase
{
    private IUsuarioRepository repository;
    private TokenService tokenService;
    public UsuarioController(IUsuarioRepository repository, TokenService tokenService)
    {
        this.repository = repository;
        this.tokenService = tokenService;
    }
      
    [HttpGet]
    [Route("VerificarUsuario")]
    public ActionResult<string> VerificarUsuario(string email, string senha)
    {
        repository.VerificarUsuario(email, senha);

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
        repository.CadastrarUsuario(usuario);
    }


}
