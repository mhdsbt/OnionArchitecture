namespace WebApi.Configuration
{
    public class MessageBusConfiguration
    {
        public string Host { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string VirtualHost { get; set; }
        public string Exchange { get; set; }
        public string ExchangeType { get; set; }
        public int Port { get; set; }
        public int PrefetchLimit { get; set; }
        public int MessageFailedTryLimit { get; set; }
    }
}
