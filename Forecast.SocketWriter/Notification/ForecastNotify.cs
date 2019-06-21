using System.Threading.Tasks;
using Forecast.SocketWriter.Hub;
using Microsoft.AspNetCore.SignalR;
using Serilog;

namespace Forecast.SocketWriter.Notification
{
    public class ForecastNotify: INotifyService
    {
        private readonly IHubContext<ForecastHub> _hub;
        private readonly ILogger _logger;
        
        public ForecastNotify(IHubContext<ForecastHub> hub, ILogger logger)
        {
            _hub = hub;
            _logger = logger;
        }
        
        public async Task SendNotificationAsync(string message)
        {
            await _hub.Clients.All.SendAsync("ReceiveMessage", message);
        }
    }
}