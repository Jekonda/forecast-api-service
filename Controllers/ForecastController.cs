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
                    value.CreatedOn = DateTime.Now;
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
                        CreatedOn = DateTime.Now,
                        LogBody = $"Exception was thrown: {e.Message}"
                    });
                }
            }
            else
            {
                await _crudRepository.Save(new Logs
                {
                    Level = "Error",
                    CreatedOn = DateTime.Now,
                    LogBody = "Request body has wrong format"
                });
            }
        }

        [HttpGet("logs")]
        public List<Logs> GetLogs() =>_crudRepository.GetAll<Logs>().ToList();

        [HttpGet("data")]
        public List<Forecast> GetForecast() => _crudRepository.GetAll<Forecast>().ToList();
        

    }
}