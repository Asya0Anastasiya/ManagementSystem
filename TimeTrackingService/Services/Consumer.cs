using AutoMapper;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
using TimeTrackingService.Interfaces.Services;
using TimeTrackingService.Models.Dto;
using TimeTrackingService.Models.Entities;

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
                HostName = "raabbitmq",
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
                var message = Encoding.UTF8.GetString(body);
                UpcomingDocumentModel? document = JsonSerializer.Deserialize<UpcomingDocumentModel>(message);
                using (var scope = Services.CreateScope())
                {
                    var documentService = scope.ServiceProvider.GetRequiredService<IDocumentService>();
                    //var timeTrackingService = scope.ServiceProvider.GetRequiredService<IDayAccountingService>();
                    //var dayAcc = await timeTrackingService.GetUserDay(document.UserId, document.Date);
                    Document doc = _mapper.Map<Document>(document);
                    //doc.DaysAccounting.Add(dayAcc);
                    await documentService.AddDocumentAsync(doc);
                }
            };

            channel.BasicConsume(queue: "documents", autoAck: true, consumer: consumer);
            return Task.CompletedTask;
        }
    }
}
