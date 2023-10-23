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
    public class Consumer : BackgroundService, IConsumer
    {
        private readonly IMessageReceiver _messageReceiver;
        private IServiceScopeFactory Services { get; }
        private readonly IMapper _mapper;

        public Consumer(IMessageReceiver messageReceiver, 
                        IServiceScopeFactory services, IMapper mapper)
        {
            _messageReceiver = messageReceiver;
            Services = services;
            _mapper = mapper;
        }

        public void StartConsuming()
        {
            var channel = _messageReceiver.CreateModel();
            channel.QueueDeclare("documents", durable: true, exclusive: false);
            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                // Обработка полученного сообщения
                Console.WriteLine("Получено сообщение: {0}", message);
            };

            channel.BasicConsume(queue: "documents", autoAck: true, consumer: consumer);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var channel = _messageReceiver.CreateModel();
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
                    var timeTrackingService = scope.ServiceProvider.GetRequiredService<IDayAccountingService>();
                    var dayAcc = await timeTrackingService.GetUserDay(document.UserId, document.Date);
                    Document doc = _mapper.Map<Document>(document);
                    //doc.DaysAccounting = new List<DayAccounting>();
                    doc.DaysAccounting.Add(dayAcc);
                    await documentService.AddDocumentAsync(doc);
                }
               
                Console.WriteLine("Получено сообщение: {0}", message);
            };

            channel.BasicConsume(queue: "documents", autoAck: true, consumer: consumer);
            return Task.CompletedTask;
        }
    }
}
