using System.Net.Mail;
using CarrefourApi.Model;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace CarrefourApi.Service
{
    public class SendGridService : IEmail
    {
        public void EnviarEmail(Programa programa, List<Inscricao> emails)
        {
            throw new NotImplementedException();
        }
    }
}