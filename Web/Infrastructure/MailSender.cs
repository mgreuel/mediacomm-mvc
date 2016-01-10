using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace MediaCommMvc.Web.Infrastructure
{
    public class MailSender
    {
        private readonly MailConfig mailConfig;

        public MailSender(MailConfig mailConfig)
        {
            this.mailConfig = mailConfig;
        }

        public void SendMail(string subject, string body, IList<string> recipients)
        {
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