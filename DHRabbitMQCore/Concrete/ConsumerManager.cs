using DHRabbitMQCore.Abstract;
using DHRabbitMQCore.Consts;
using DHRabbitMQCore.Entities;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Reflection;
using System.Text;

namespace DHRabbitMQCore.Concrete
{
    public class ConsumerManager : IConsumerService
    {
        private SemaphoreSlim _semaphore;

        private EventingBasicConsumer _consumer;
        private IModel _channel;
        private IConnection _connection;

        private readonly IRabbitMQService _rabbitMQServices;
        private readonly IMailSender _mailSender;

        public ConsumerManager(
            IRabbitMQService rabbitMQServices,
            IMailSender mailSender
            )
        {
            _rabbitMQServices = rabbitMQServices;
            _mailSender = mailSender ?? throw new ArgumentNullException(nameof(mailSender));
        }

        public void Start()
        {
            try
            {
                //_semaphore = new SemaphoreSlim(RabbitMQConsts.ParallelThreadsCount);
                _connection = _rabbitMQServices.GetConnection();
                _channel = _rabbitMQServices.GetModel(_connection);

                _channel.QueueDeclare(queue: RabbitMQConsts.RabbitMqConstsList.QueueNameEmail.ToString(),
                                    durable: true,
                                    exclusive: false,
                                    autoDelete: false,
                                    arguments: null);

                _channel.BasicQos(0, RabbitMQConsts.ParallelThreadsCount, false);
                _consumer = new EventingBasicConsumer(_channel);
                _consumer.Received += Consumer_Received;

                _channel.BasicConsume(queue: RabbitMQConsts.RabbitMqConstsList.QueueNameEmail.ToString(),
                                           autoAck: true,
                                           consumer: _consumer);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        private void Consumer_Received(object sender, BasicDeliverEventArgs ea)
        {
            try
            {
                MailMessageData message = JsonConvert.DeserializeObject<MailMessageData>(Encoding.UTF8.GetString(ea.Body.ToArray()));
                try
                {
                    Task.Run(() =>
                    {
                        _mailSender.SendMail(message);
                    });
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message.ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Consumer_Received exception => " + ex.Message);
            }
        }


    }
}
