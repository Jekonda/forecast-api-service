using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ReceivingService.Models;
using ReceivingService.Services.Repository;
using Serilog;

namespace ReceivingService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ForecastController : ControllerBase
    {
        private readonly ICrudRepository _crudRepository;
        private readonly ILogger<ForecastController> _logger;
        
        public ForecastController(ICrudRepository crudRepository, ILogger<ForecastController> logger)
        {
            _crudRepository = crudRepository;
            _logger = logger;
        }

        [HttpPost("send")]
        public async Task Post([FromBody] Forecast value)
        {
            if (value != null)
            {
                try
                { 
                    value.CreatedOn = DateTime.Now;
                    await _crudRepository.Save(value);
                    _logger.LogInformation("Item successfully saved", value);
                }
                catch (Exception e)
                {
                    _logger.LogError("Could not save data", e);
                }
            }
            else
            {
                _logger.LogWarning("Value is null");
            }
        }

        [HttpGet("data")]
        public IEnumerable<Forecast> GetForecast() => _crudRepository.GetAll<Forecast>();

        [HttpGet("actual")]
        public Forecast GetActual() => _crudRepository.GetAll<Forecast>().Last();

        [HttpGet("today")]
        public IEnumerable<Forecast> GetTodayData()
        {
            var startDateTime = DateTime.Today; //Today at 00:00:00
            var endDateTime = DateTime.Today.AddDays(1).AddTicks(-1); //Today at 23:59:59
            return _crudRepository.GetAll<Forecast>().Where(d => d.CreatedOn >= startDateTime &&
                                                                 d.CreatedOn <= endDateTime);
        }

        [HttpGet("ping")]
        public string Ping() => "Pong";
    }
}