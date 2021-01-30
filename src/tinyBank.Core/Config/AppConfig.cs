namespace tinyBank.Core.Config
{
    public class AppConfig
    {
        public string TinyBankConnectionString { get; set; }
        public string MinLoggingLevel { get; set; }
        public ClientConfig ClientConfig { get; set; }
    }
}
