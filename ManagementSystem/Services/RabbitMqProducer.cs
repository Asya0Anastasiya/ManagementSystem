using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;
using UserService.Interfaces.Services;
using UserService.Options;

namespace UserService.Services
{
    public class RabbitMqProducer : IMessageProducer
    {
        private readonly IConnection _connection;
        private readonly RabbitMqOptions _rabbitMqOptions;

        public RabbitMqProducer(IOptions<RabbitMqOptions> rabbitMqOptions)
        {
            _rabbitMqOptions = rabbitMqOptions.Value;

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

            channel.QueueDeclare(_rabbitMqOptions.QueueName, durable: true, exclusive: false);

            var json = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(json);

            channel.BasicPublish(exchange: "", routingKey: _rabbitMqOptions.QueueName, body: body);
        }
    }
}
