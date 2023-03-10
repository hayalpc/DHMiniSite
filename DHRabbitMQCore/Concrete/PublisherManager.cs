using DHRabbitMQCore.Abstract;
using DHRabbitMQCore.Consts;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace DHRabbitMQCore.Concrete
{
    public class PublisherManager : IPublisherService
    {
        private readonly IRabbitMQService _rabbitMQServices;
        private IModel _channel;
        private IConnection _connection;

        public PublisherManager(IRabbitMQService rabbitMQServices) => _rabbitMQServices = rabbitMQServices;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queueDataModels">Herhangi bir tipte gönderilebilir where koşullaırına uyan</param>
        /// <param name="queueName">Queue kuyrukta hangi isimde tutulacağı bilgisi operasyon istek zamanı gönderilebilir.</param>
        public void Enqueue<T>(IEnumerable<T> queueDataModels, string queueName) where T : class, new()
        {
            try
            {
                _connection = _rabbitMQServices.GetConnection();
                using (_channel = _rabbitMQServices.GetModel(_connection))
                {
                    _channel.QueueDeclare(queue: queueName,
                                         durable: true,      // ile in-memory mi yoksa fiziksel olarak mı saklanacağı belirlenir.
                                         exclusive: false,   // yalnızca bir bağlantı tarafından kullanılır ve bu bağlantı kapandığında sıra silinir - özel olarak işaretlenirse silinmez
                                         autoDelete: false,  // en son bir abonelik iptal edildiğinde en az bir müşteriye sahip olan kuyruk silinir
                                         arguments: null);   // isteğe bağlı; eklentiler tarafından kullanılır ve TTL mesajı, kuyruk uzunluğu sınırı, vb. 
                    
                    //var properties = _channel.CreateBasicProperties();
                    //properties.Persistent = true;
                    //properties.Expiration = RabbitMQConsts.MessagesTTL.ToString();

                    foreach (var queueDataModel in queueDataModels)
                    {
                        var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(queueDataModel));
                        _channel.BasicPublish(exchange: "",
                                             routingKey: queueName,
                                             //basicProperties: properties,
                                             body: body);
                    }
                }
            }
            catch (Exception ex)
            {
                //loglama yapılabilir
                throw new Exception(ex.Message.ToString());
            }
        }
    }
}


