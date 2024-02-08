using EmailService.Interfaces;
using EmailService.Models;
using EmailService.Models.Enums;
using EmailService.Models.Messages;
using EmailService.Options;
using Microsoft.Extensions.Options;
using Org.BouncyCastle.Crypto;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace EmailService.Services
{
    public class Consumer : BackgroundService
    {
        private IServiceScopeFactory Services { get; }
        private readonly IConnection _connection;
        private readonly RabbitMqOptions _rabbitMqOptions;

        public Consumer(IServiceScopeFactory services, IOptions<RabbitMqOptions> rabbitMqOptions)
        {
            Services = services;
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

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var channel = _connection.CreateModel();
            channel.QueueDeclare(_rabbitMqOptions.QueueName, durable: true, exclusive: false);
            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var content = Encoding.UTF8.GetString(body);
                var message = JsonSerializer.Deserialize<BaseMessage>(content);

                try
                {
                    switch (message.MessageType)
                    {
                        case MessageTypes.NewUserAdded:
                            var newUserAdded = JsonSerializer.Deserialize<NewUserAddedMessage>(content);

                            using (var scope = Services.CreateScope())
                            {
                                var emailService = scope.ServiceProvider.GetRequiredService<IEmailSender>();

                                Message email = new(new string[] { newUserAdded.UserCredentials.Email }, 
                                    "Welcome!", $"Here is your temporary password: {newUserAdded.UserCredentials.Password}");

                                emailService.SendEmail(email);
                            }

                            break;
                        default:
                            break;
                    }
                }
                catch (Exception)
                {
                    channel.BasicAck(ea.DeliveryTag, false);
                }
            };

            channel.BasicConsume(queue: _rabbitMqOptions.QueueName, autoAck: true, consumer: consumer);
            return Task.CompletedTask;
        }
    }
}
