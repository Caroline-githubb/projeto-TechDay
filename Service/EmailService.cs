using System.Net;
using System.Net.Mail;
using CarrefourApi.Model;

namespace CarrefourApi.Service
{
    public class EmailService
    {
        
        public void EnviarEmail(Programa programa, List<Inscricao> emails)
        {
            try
            {
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
                message.Body = "Programa " + programa.Nome + ": " + programa.Tema + "\n\nDescrição: " + programa.Descricao + "\n Site: " + programa.Site;

                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("ingrid.caroline.teste@gmail.com", "gdheeoctdoogcqwo");
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
            }

            catch (Exception ex)
            {
                Console.WriteLine("Erro ao mandar o  email : " + ex.Message);
                throw new Exception("Erro ao enviar email : " + ex.Message);
            }
        }

    }
}