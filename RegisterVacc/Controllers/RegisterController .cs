using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RegisterVacc.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace RegisterVacc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly MessageService _service;

        public RegisterController(MessageService service)
        {
            _service = service;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Guid?))]
        public async Task<IActionResult> RegisterPerson(string FirstName, string LastName)
        {
            var guid = Guid.NewGuid();
            int amount = -1;
            using (var client = new HttpClient())
            {
                amount = int.Parse(await client.GetStringAsync("http://localhost:5003/api/Vaccine"));
            }
            await _service.SendMessage(new MessagePayload { EventName="New_Registration" });
            if (amount > 0)
            {
                return Created($"GUID: {guid}", guid);
            }
            else
                return Ok(String.Empty);
        }
    }
}
