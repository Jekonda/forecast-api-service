using System.Threading.Tasks;

namespace Forecast.DataReceiver.Services.Messages
{
    public interface IQueueSender
    {
        Task SendAsync(object message, string queueName = null);
    }
}