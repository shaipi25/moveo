﻿namespace Gateway
{
    public class RabbitMQSettings
    {
        public string HostName { get; set; }
        public int Port { get; set; } = 5672;
        public string UserName { get; set; }
        public string Password { get; set; }
        public string VirtualHost { get; set; } = "/";
    }
}
