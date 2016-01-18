namespace MediaCommMvc.Web.Infrastructure
{
    public class Config
    {
        public const string ConfigId = "Config";

        public string Id => ConfigId;

        public string Sitename { get; set; }

        public string PhotoStorageRootFolder { get; set; }

        public string RegistrationCode { get; set; }

        public string BaseUrl { get; set; }
    }
}
