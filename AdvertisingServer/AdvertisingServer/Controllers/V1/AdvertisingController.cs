using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AdvertisingServer.Infrastructure.Interfaces;
using AdvertisingServer.Models.DbContext;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace AdvertisingServer.Controllers.V1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class AdvertisingController : ControllerBase
    {
        private readonly IAdvertisingService _adService;

        public AdvertisingController(IAdvertisingService advertisingService)
        {
            _adService = advertisingService;
        }

        // GET api/values
        [HttpGet]
        [SwaggerOperation(nameof(Get))]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(Advertising), "Returns all yours advertising")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, description: "Resturn 404 status elements not found")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, description: "Returns 400 if not all parameters were specified")]
        public ActionResult<IEnumerable<Advertising>> Get()
        {
            var result = new Advertising();

            return Ok(result);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
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
