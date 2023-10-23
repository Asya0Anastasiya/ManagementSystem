using Microsoft.EntityFrameworkCore.Metadata;
using RabbitMQ.Client;

namespace TimeTrackingService.Interfaces.Services
{
    public interface IMessageReceiver
    {
        IConnection GetConnection();

        RabbitMQ.Client.IModel CreateModel();
    }
}
