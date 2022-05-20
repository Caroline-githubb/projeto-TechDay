using System.Net;
using CarrefourApi.Model;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;


namespace CarrefourApi.Service
{
    public class SendGridService : IEmail
    {
        private readonly ISendGridClient _sendGridClient;
        private readonly IConfiguration _configuration;
        public SendGridService(ISendGridClient sendGridClient,IConfiguration configuration)
        {
            _sendGridClient = sendGridClient;
            _configuration = configuration;
        }
        
        public void EnviarEmail(Programa programa, List<Inscricao> emails)
        {
            try
            {
                string fromEmail = _configuration.GetSection("SendGridEmailSettings").GetValue<string>("FromEmail");                          
                
                var message = new SendGridMessage()
                {
                    From = new EmailAddress(fromEmail),
                    Subject = "NOVIDADE NO MUNDO DA TI PARA AS MULHERES", //Assunto                
                    PlainTextContent = "Programa " + programa.Nome + ": " + programa.Tema + "\n\nDescrição: " + programa.Descricao + "\n Site: " + programa.Site
                };

                foreach (var email in emails)
                {
                    message.AddTo(new EmailAddress(email.Email));
                }
                var response = _sendGridClient.SendEmailAsync(message);
            }

            catch (Exception ex)
            {
                Console.WriteLine("Erro ao mandar o  email : " + ex.Message);
                throw new Exception("Erro ao enviar email : " + ex.Message);
            }
        }
    }
}