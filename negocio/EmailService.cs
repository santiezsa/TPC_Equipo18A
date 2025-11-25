using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;
using System.Configuration;

namespace negocio
{
    public class EmailService
    {
        private MailMessage email;
        private SmtpClient server;

        string emailRemitente = "sorteosutnfrgp@gmail.com";
        string passwordRemitente = "vndz ynfs siso lsrq";

        public EmailService()
        {
            server = new SmtpClient();
            server.Credentials = new NetworkCredential(emailRemitente, passwordRemitente);
            server.EnableSsl = true;
            server.Port = 587;
            server.Host = "smtp.gmail.com";
        }

        public void armarCorreo(string emailDestino, string asunto, string cuerpo)
        {
            email = new MailMessage();
            email.From = new MailAddress(emailRemitente, "Comercio Grupo 18A");
            email.To.Add(emailDestino);
            email.Subject = asunto;
            email.IsBodyHtml = true;
            email.Body = cuerpo;
        }

        public void enviarEmail()
        {
            try
            {
                server.Send(email);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
