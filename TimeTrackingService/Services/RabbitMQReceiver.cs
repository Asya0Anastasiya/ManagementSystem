using RabbitMQ.Client;
using TimeTrackingService.Interfaces.Services;

namespace TimeTrackingService.Services
{
    public class RabbitMQReceiver : IMessageReceiver
    {
        private readonly IConnection _connection;

        public RabbitMQReceiver()
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

            _connection = factory.CreateConnection();
        }

        public IConnection GetConnection()
        {
            return _connection;
        }

        public IModel CreateModel()
        {
            return _connection.CreateModel();
        }
    }
}
