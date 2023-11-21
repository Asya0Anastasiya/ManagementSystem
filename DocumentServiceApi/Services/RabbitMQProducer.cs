using DocumentServiceApi.Interfaces.Services;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace DocumentServiceApi.Services
{
    public class RabbitMQProducer : IMessageProducer
    {
        private readonly IConnection _connection;

        public RabbitMQProducer() 
        {
            ConnectionFactory factory = new()
            {
                HostName = "localhost",
                Port = 5672,
                UserName = "asiya",
                Password = "password"
            };  

            _connection = factory.CreateConnection();
        }

        public void SendMessage<T>(T message)
        {
            using var channel = _connection.CreateModel();

            channel.QueueDeclare("documents", durable: true, exclusive: false);

            var json = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(json);

            channel.BasicPublish(exchange: "", routingKey: "documents", body: body);
        }
    }
}
