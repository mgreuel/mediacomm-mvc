namespace MediaCommMvc.Web.Infrastructure
{
    public class MailConfiguration
    {
        public string SmtpHost { get; set; }

        public string MailFrom { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }
    }
}