using DocumentServiceApi.Interfaces.Services;
using DocumentServiceApi.Options;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace DocumentServiceApi.Services
{
    public class RabbitMQProducer : IMessageProducer
    {
        private readonly IConnection _connection;
        private readonly RabbitMqOptions _rabbitMqOptions;
        private readonly RabbitMqQueueOptions _rabbitMqQueueOptions;

        public RabbitMQProducer(IOptions<RabbitMqOptions> rabbitMqOptions, 
                                IOptions<RabbitMqQueueOptions>  rabbitMqQueueOptions) 
        {
            _rabbitMqOptions = rabbitMqOptions.Value;
            _rabbitMqQueueOptions = rabbitMqQueueOptions.Value;

            ConnectionFactory factory = new()
            {
                HostName = _rabbitMqOptions.HostName,
                Port = _rabbitMqOptions.Port,
                UserName = _rabbitMqOptions.UserName,
                Password = _rabbitMqOptions.Password
            };  

            _connection = factory.CreateConnection();
        }

        public void SendMessage<T>(T message)
        {
            using var channel = _connection.CreateModel();

            channel.QueueDeclare(_rabbitMqQueueOptions.QueueName, durable: true, exclusive: false);

            var json = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(json);

            channel.BasicPublish(exchange: "", routingKey: _rabbitMqQueueOptions.QueueName, body: body);
        }
    }
}
