using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
        
        public ForecastController(IRepository<Forecast> repository, IRepository<Logs> logsRepository)
        {
            _repository = repository;
            _logsRepository = logsRepository;
        }

        // POST api/values
        [HttpPost("send")]
        public async Task Post([FromBody] Forecast value)
        {
            if(value != null)
                await _repository.Save(value);
        }

        


    }
}
