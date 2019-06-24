using System.Threading.Tasks;
using Forecast.SocketService.Hub;
using Microsoft.AspNetCore.SignalR;
using Serilog;

namespace Forecast.SocketService.Notification
{
    public class ForecastNotification: INotifyService
    {
        private readonly IHubContext<ForecastHub> _hub;
        private readonly ILogger _logger;
        
        public ForecastNotification(IHubContext<ForecastHub> hub, ILogger logger)
        {
            _hub = hub;
            _logger = logger;
        }
        
        public async Task SendNotificationAsync(string message)
        {
            _logger.Information($"Sending information to socket: {message}");
            await _hub.Clients.All.SendAsync("ForecastData", message);
        }
    }
}