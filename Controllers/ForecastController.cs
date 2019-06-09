using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using ReceivingService.DB;
using ReceivingService.Models;
using ReceivingService.Services;

namespace ReceivingService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ForecastController : ControllerBase
    {
        private readonly IRepository<Forecast> _repository;
        private readonly IRepository<Logs> _logsRepository;
        private readonly MongoContext _context;
        
        public ForecastController(IRepository<Forecast> repository, IRepository<Logs> logsRepository, MongoContext context)
        {
            _repository = repository;
            _logsRepository = logsRepository;
            _context = context;
        }

        // POST api/values
        [HttpPost("send")]
        public async Task Post([FromBody] Forecast value)
        {
            if (value != null)
            {
                value.Time = DateTime.Now;
                try
                {
                    await _repository.Save(value);
                    await _logsRepository.Save(new Logs
                    {
                        Level = "Info",
                        Time = DateTime.Now,
                        LogBody = value.ToString()
                    });
                }
                catch (Exception e)
                {
                    await _logsRepository.Save(new Logs
                    {
                        Level = "Error",
                        Time = DateTime.Now,
                        LogBody = $"Exception was thrown: {e.Message}"
                    });
                }
            }
            else
            {
                await _logsRepository.Save(new Logs
                {
                    Level = "Error",
                    Time = DateTime.Now,
                    LogBody = "Request body has wrong format"
                });
            }
        }

        [HttpGet("logs")]
        public List<Logs> GetLogs()
        {
            var logs = _logsRepository.GetAll();
            return logs.ToList();
        }
        
        [HttpGet("data")]
        public async Task<List<Forecast>> GetForecast()
        {
//            var forecast = _repository.GetAll();
//            return forecast.ToList();
            var list = await _context.Forecast.Find(x => true).ToListAsync();
            return list;
        }

        [HttpGet("ping")]
        public string Ping()
        {
            return "Pong";
        }
    }
}
