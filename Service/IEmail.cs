using CarrefourApi.Model;

namespace CarrefourApi.Service
{
    public interface IEmail
    {
         void EnviarEmail(Programa programa, List<Inscricao> emails);
    }
}