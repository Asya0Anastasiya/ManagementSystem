using AutoMapper;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json;
using TimeTrackingService.Interfaces.Services;
using TimeTrackingService.Models.Entities;
using TimeTrackingService.Models.Enums;
using TimeTrackingService.Models.Messages;
using Document = TimeTrackingService.Models.Entities.Document;

namespace TimeTrackingService.Services
{
    public class Consumer : BackgroundService
    {
        private IServiceScopeFactory Services { get; }
        private readonly IMapper _mapper;
        private readonly IConnection _connection;

        public Consumer(IServiceScopeFactory services, IMapper mapper)
        {
            Services = services;
            _mapper = mapper;
            ConnectionFactory factory = new()
            {
                HostName = "localhost",
                Port = 5672,
                UserName = "asiya",
                Password = "password"
            };

            _connection = factory.CreateConnection();
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var channel = _connection.CreateModel();
            channel.QueueDeclare("documents", durable: true, exclusive: false);
            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var content = Encoding.UTF8.GetString(body);
                var message = JsonSerializer.Deserialize<BaseMessage>(content);

                try
                {
                    switch(message.MessageType)
                    {
                        case MessageTypes.TimeTrackDocumentUploaded:
                            var documentUploadedMessage = JsonSerializer.Deserialize<TimeTrackDocumentUploadedMessage>(content);

                            using (var scope = Services.CreateScope())
                            {
                                var documentService = scope.ServiceProvider.GetRequiredService<IDocumentService>();
                                Document doc = _mapper.Map<Document>(documentUploadedMessage.DocumentModel);
                                await documentService.AddDocumentAsync(doc);
                            }

                            break;
                    }
                }
                catch (Exception)
                {
                    channel.BasicAck(ea.DeliveryTag, false);
                }
            };

            channel.BasicConsume(queue: "documents", autoAck: true, consumer: consumer);
            return Task.CompletedTask;
        }
    }
}
