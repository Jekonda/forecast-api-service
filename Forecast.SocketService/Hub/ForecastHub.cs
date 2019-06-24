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

        public Task SendMessage(string msg)
        {
            return Clients.All.SendAsync("ForecastData", msg);
        }
    }
}