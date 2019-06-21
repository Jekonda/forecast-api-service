using System.Threading.Tasks;

namespace Forecast.SocketWriter.Notification
{
    public interface INotifyService
    {
        Task SendNotificationAsync(string message);
    }
}