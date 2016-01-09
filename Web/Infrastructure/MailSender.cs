using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace MediaCommMvc.Web.Infrastructure
{
    public class MailSender
    {
        private readonly MailConfiguration mailConfiguration;

        public MailSender(MailConfiguration mailConfiguration)
        {
            this.mailConfiguration = mailConfiguration;
        }

        public void SendMail(string subject, string body, IList<string> recipients)
        {
            var smtp = new SmtpClient
            {
                Host = this.mailConfiguration.SmtpHost,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials =
                    new System.Net.NetworkCredential(this.mailConfiguration.Username, this.mailConfiguration.Password)
            };

            using (MailMessage message = new MailMessage(this.mailConfiguration.MailFrom, recipients.First()) { Subject = subject, Body = body })
            {
                message.IsBodyHtml = true;
                recipients.ToList().ForEach(r => message.Bcc.Add(r));
                smtp.Send(message);
            }
        }
    }
}