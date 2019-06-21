using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Serilog;

namespace Forecast.SocketService.Hub
{
    public class ForecastHub: Microsoft.AspNetCore.SignalR.Hub
    {
        private readonly ILogger _logger;

        public ForecastHub(ILogger logger)
        {
            _logger = logger;
        }

        public Task SendMessage(string user, string message)
        {
            return Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}