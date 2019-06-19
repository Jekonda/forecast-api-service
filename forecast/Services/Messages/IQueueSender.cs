using System.Threading.Tasks;

namespace forecast.Services.Messages
{
    public interface IQueueSender
    {
        Task SendAsync(object message, string queueName = null);
    }
}