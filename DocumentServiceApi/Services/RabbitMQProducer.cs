using DocumentServiceApi.Interfaces.Services;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace DocumentServiceApi.Services
{
    public class RabbitMQProducer : IMessageProducer
    {
        public void SendMessage<T>(T message)
        {
            //var factory = new ConnectionFactory 
            //{ 
            //    HostName = "localhost",
            //    UserName = "asiya",
            //    Password = "password",
            //    VirtualHost = "/"
            //};

            ConnectionFactory factory = new() { HostName = "localhost", Port = 5672 };
            factory.UserName = "asiya";
            factory.Password = "password";


            var connection = factory.CreateConnection();

            using var channel = connection.CreateModel();

            channel.QueueDeclare("documents", durable: true, exclusive: false);

            var json = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(json);

            channel.BasicPublish(exchange: "", routingKey: "documents", body: body);
        }
    }
}
