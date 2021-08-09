using Cohesion.Core.ServiceRequest.Models;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Cohesion.Core.ServiceRequest.Utilities
{
    public class EmailUtility : IEmailUtility
    {
        private readonly SMTPConfig smtpConfig;
        public EmailUtility(IOptions<SMTPConfig> smtpConfig)
        {
            this.smtpConfig = smtpConfig.Value;
        }
        public void SendEmail(string subject, string body, string email)
        {
            MailMessage mail = new MailMessage
            {
                Subject = subject,
                Body = body,
                From = new MailAddress(smtpConfig.SenderAddress, smtpConfig.SenderDisplayName),
                IsBodyHtml = smtpConfig.IsBodyHtml
            };

            mail.To.Add(email);

            NetworkCredential networkCredential = new NetworkCredential(smtpConfig.UserName, smtpConfig.Password);

            SmtpClient smtpClient = new SmtpClient
            {
                Host = smtpConfig.Host,
                Port = smtpConfig.Port,
                EnableSsl = smtpConfig.EnableSSL,
                UseDefaultCredentials = smtpConfig.UseDefaultCredentials,
                Credentials = networkCredential
            };

            mail.BodyEncoding = Encoding.Default;

            smtpClient.Send(mail);
        }
    }
}
