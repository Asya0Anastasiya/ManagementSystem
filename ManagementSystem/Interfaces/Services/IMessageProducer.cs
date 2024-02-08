namespace UserService.Interfaces.Services
{
    public interface IMessageProducer
    {
        void SendMessage<T>(T message);
    }
}
