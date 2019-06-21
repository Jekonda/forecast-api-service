using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Forecast.DataReceiver.Services.Repository;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Forecast.DataReceiver;
using Forecast.DataReceiver.Models;


namespace Forecast.DataReceiver.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ForecastController : ControllerBase
    {
        private readonly ICrudRepository _crudRepository;
        private readonly ILogger _logger;

        public ForecastController(ICrudRepository crudRepository, ILogger logger)
        {
            _crudRepository = crudRepository;
            _logger = logger;
        }

        [HttpPost("send")]
        public async Task Post([FromBody] Models.Forecast value)
        {
            try
            {
                value.CreatedOn = DateTime.Now;
                await _crudRepository.Save(value);
                _logger.Information($"Item successfully saved: {value}");
            }
            catch (Exception e)
            {
                _logger.Error($"Could not save data: {e}");
            }
        }

        [HttpGet("data")]
        public IEnumerable<Models.Forecast> GetForecast() => _crudRepository.GetAll<Models.Forecast>();

        [HttpGet("actual")]
        public Models.Forecast GetActual() => _crudRepository.GetAll<Models.Forecast>().LastOrDefault();

        [HttpGet("today")]
        public IEnumerable<Models.Forecast> GetTodayData()
        {
            var endDateTime = DateTime.Today.AddDays(1).AddTicks(-1); //Today at 23:59:59
            return _crudRepository.GetAll<Models.Forecast>()
                .Where(d => d.CreatedOn >= DateTime.Today && d.CreatedOn <= endDateTime);
        }

        [HttpGet("ping")]
        public string Ping() => "Pong";
    }
}