using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Warehouse.Services;

namespace Warehouse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VaccineController : ControllerBase
    {

        [HttpGet]
        public  IActionResult getVaccine()
        {
            var count = VaccStorage.getVacAmount();
            VaccStorage.reduceVaccAmount();
            
            return Ok(count);
        }
    }
}
