using DHRabbitMQCore.Abstract;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace DHRabbitMQCore.Concrete
{
    public class RabbitMQConfiguration : IRabbitMQConfiguration
    {
        public IConfiguration Configuration { get; }
        public RabbitMQConfiguration(IConfiguration configuration) => Configuration = configuration;

        public string HostName => Configuration.GetSection("RabbitMQConfiguration:HostName").Value;
        public string UserName => Configuration.GetSection("RabbitMQConfiguration:UserName").Value;
        public string Password => Configuration.GetSection("RabbitMQConfiguration:Password").Value;
    }
}
