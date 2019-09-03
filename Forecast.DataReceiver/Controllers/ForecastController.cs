using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Forecast.DataReceiver.Models;
using Forecast.DataReceiver.Services.Repository;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Forecast.DataReceiver.Services.Messages;
using Microsoft.EntityFrameworkCore;

namespace Forecast.DataReceiver.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ForecastController : ControllerBase
    {
        private readonly ICrudRepository _crudRepository;
        private readonly ILogger _logger;
        private readonly IQueueSender _queueSender;

        public ForecastController(ICrudRepository crudRepository, ILogger logger, IQueueSender queueSender)
        {
            _crudRepository = crudRepository;
            _logger = logger;
            _queueSender = queueSender;
        }

        [HttpPost("send")]
        public async Task Post([FromBody] Prediction value)
        {
            try
            {
                value.CreatedOn = DateTime.Now;
                await _crudRepository.SaveAsync(value);
                _logger.Information($"Item successfully saved: {value}");
                await _queueSender.SendAsync(value.ToString());
            }
            catch (Exception e)
            {
                _logger.Error($"Could not save data: {e}");
            }
        }

        [HttpGet("data")]
        public async Task<List<Prediction>> GetForecast()
        {
            return await _crudRepository.ToListAsync<Prediction>();
        }

        [HttpGet("actual")]
        public async Task<Prediction> GetActual()
        {
            return await _crudRepository.AsQueryable<Prediction>().LastOrDefaultAsync();
        }

        [HttpGet("today")]
        public async Task<List<Prediction>> GetTodayData()
        {
            var endDateTime = DateTime.Today.AddDays(1).AddTicks(-1); //Today at 23:59:59
            return await _crudRepository.AsQueryable<Prediction>()
                .Where(d => d.CreatedOn >= DateTime.Today && d.CreatedOn <= endDateTime).ToListAsync();
        }

        [HttpGet("ping")]
        public string Ping() => "Pong";
    }
}