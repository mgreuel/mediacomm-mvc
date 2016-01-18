namespace MediaCommMvc.Web.Infrastructure
{
    public class MailConfig
    {
        public const string MailConfigId = "MailConfig";

        public string Id => MailConfigId; 

        public string SmtpHost { get; set; }

        public string MailFrom { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }
    }
}