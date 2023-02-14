using System.Collections.Generic;

namespace DHRabbitMQCore.Abstract
{
    public interface IPublisherService
    {
        void Enqueue<T>(IEnumerable<T> queueDataModels, string queueName ) where T: class, new();
    }
}

