using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace DHRabbitMQCore.Abstract
{
    public interface IRabbitMQService
    {
        IConnection GetConnection();
        IModel GetModel(IConnection connection);
    }
}
