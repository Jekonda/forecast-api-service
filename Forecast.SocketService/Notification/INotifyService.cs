using System.Threading.Tasks;

namespace Forecast.SocketService.Notification
{
    public interface INotifyService
    {
        Task SendNotificationAsync(string message);
    }
}