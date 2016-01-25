using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;

namespace MediaCommMvc.Web.Infrastructure
{
    public class MailSender
    {
        private readonly MailConfig mailConfig;

        private readonly ILogger logger;

        public MailSender(MailConfig mailConfig, ILogger logger)
        {
            this.mailConfig = mailConfig;
            this.logger = logger;
        }

        public void SendMail(string subject, string body, IList<string> recipients)
        {
            this.logger.Info($"Sending notification '{subject}' to '{string.Join(",", recipients)}'");
            
            var smtp = new SmtpClient
            {
                Host = this.mailConfig.SmtpHost,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials =
                    new System.Net.NetworkCredential(this.mailConfig.Username, this.mailConfig.Password)
            };

            using (MailMessage message = new MailMessage(this.mailConfig.MailFrom, recipients.First()) { Subject = subject, Body = body })
            {
                message.IsBodyHtml = true;
                recipients.ToList().ForEach(r => message.Bcc.Add(r));
                smtp.Send(message);
            }
        }
    }
}