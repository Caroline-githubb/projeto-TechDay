using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CarrefourApi.Model;
using Microsoft.IdentityModel.Tokens;

namespace CarrefourApi.Security
{
    public class TokenService
    {
        public static string Secret = "QK68aOyyF13ZuNeQfleseFJIZoiWspkzZVyjOX_8qnfzQuf_3djl9hxMItlpEXoPQpJTr-NHimCusejbUZOwOE_sduNBF76UJSKESBhm1cSK0lP1wZzL3ZO4QCVrMNVE0AUvlox2MN3l6RzQeiXpUjVwqTCOVJTUpVeNGcCQ0gUPVLDlJpZiSKKzrvUOuolS3-OVpHU3wJG3EvJLKr8fjrGrdcF8nFK06utDBSZz7iczBZsijhEjA9HqYdqQbSFtANJBqB_DeYt7r2uawBAdCnhY-LhhY_2aEC4f5XRvj3WUhbkzoM14usk9KYos8lwAXB8fgTjGlNzcL3sOWF9j3w";

        public string GenerateToken(string login)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, login),
                    new Claim(ClaimTypes.Role, "Administrador")
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}