using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Raspberry.GPIO;

namespace GpioWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GpiosController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new ActionResult<IEnumerable<string>>(GpioPinManager.Instance.Pins);
        }

        // GET api/values/5
        [HttpGet("{port}")]
        public ActionResult<string> Get(int port)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
