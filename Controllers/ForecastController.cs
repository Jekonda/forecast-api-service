using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ReceivingService.Models;
using ReceivingService.Services.Repository;

namespace ReceivingService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ForecastController : ControllerBase
    {
        private readonly CrudRepository _crudRepository;

        public ForecastController(CrudRepository crudRepository)
        {
            _crudRepository = crudRepository;
        }

        // POST api/values
        [HttpPost("send")]
        public async Task Post([FromBody] Forecast value)
        {
            if (value != null)
            {
                try
                {
                    await _crudRepository.Save(value);
                    await _crudRepository.Save(new Logs
                    {
                        Level = "Info",
                        LogBody = value.ToString()
                    });
                }
                catch (Exception e)
                {
                    await _crudRepository.Save(new Logs
                    {
                        Level = "Error",
                        LogBody = $"Exception was thrown: {e.Message}"
                    });
                }
            }
            else
            {
                await _crudRepository.Save(new Logs
                {
                    Level = "Error",
                    LogBody = "Request body has wrong format"
                });
            }
        }

        [HttpGet("logs")]
        public List<Logs> GetLogs()
        {
            var logs = _crudRepository.GetAll<Logs>();
            return logs.ToList();
        }

        [HttpGet("data")]
        public List<Forecast> GetForecast()
        {
            var forecast = _crudRepository.GetAll<Forecast>();
            return forecast.ToList();
        }

    }
}