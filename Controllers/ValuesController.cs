using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ReceivingService.Models;

namespace ReceivingService.Controllers
{
    [Route("api")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // POST api/values
        [HttpPost("send")]
        public void Post([FromBody] Forecast value)
        {
            
            
        }



    }
}
