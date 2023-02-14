using System;
using System.Collections.Generic;
using System.Text;

namespace DHRabbitMQCore.Abstract
{
    public interface IRabbitMQConfiguration
    { 
        string HostName { get; }
        string UserName { get; }
        string Password { get; }
    }
}
