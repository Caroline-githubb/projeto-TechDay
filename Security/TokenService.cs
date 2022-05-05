using System.IdentityModel.Tokens.Jwt; 
// O JWT nada mais é que o armazenamento das informações do token no formato JSON.
using System.Security.Claims;
using System.Text;
using CarrefourApi.Model;
using Microsoft.IdentityModel.Tokens;

namespace CarrefourApi.Security
{
    public class TokenService
    {
        //chave privada que só a API tem acesso para gerar o TOKEN (é gerado por site)
        public static string Secret = "QK68aOyyF13ZuNeQfleseFJIZoiWspkzZVyjOX_8qnfzQuf_3djl9hxMItlpEXoPQpJTr-NHimCusejbUZOwOE_sduNBF76UJSKESBhm1cSK0lP1wZzL3ZO4QCVrMNVE0AUvlox2MN3l6RzQeiXpUjVwqTCOVJTUpVeNGcCQ0gUPVLDlJpZiSKKzrvUOuolS3-OVpHU3wJG3EvJLKr8fjrGrdcF8nFK06utDBSZz7iczBZsijhEjA9HqYdqQbSFtANJBqB_DeYt7r2uawBAdCnhY-LhhY_2aEC4f5XRvj3WUhbkzoM14usk9KYos8lwAXB8fgTjGlNzcL3sOWF9j3w";

        public string GenerateToken(string login)
        {
            var tokenHandler = new JwtSecurityTokenHandler(); //cria um objeto que gera um token
            var key = Encoding.ASCII.GetBytes(Secret);//transforma o secret em um array de bytes
            var tokenDescriptor = new SecurityTokenDescriptor 
            // token a ser gerado, com os Claims a data de expiração e as credenciais de acesso.
            {
                Subject = new ClaimsIdentity(new Claim[] //define o Claim principal (no caso Administrador)
                {
                    new Claim(ClaimTypes.Name, login),
                    new Claim(ClaimTypes.Role, "Administrador")
                }),
                Expires = DateTime.UtcNow.AddHours(2),//expira a cada duas horas
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);//cria o token com as especificações definidas acima
            return tokenHandler.WriteToken(token);//transforma o objeto token em string
        }
    }
}