using AdvertisingServer.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AdvertisingServer.Constants;
using AdvertisingServer.Models.Dto.Advertising;

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
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(AdvertisingBase), "Returns all yours advertising")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, description: "Resturn 404 status elements not found")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, description: "Returns 400 if not all parameters were specified")]
        public async Task<ActionResult<IEnumerable<AdvertisingBase>>> Get(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                return BadRequest(ResponseMessages.NotAllParametersSpecified);
            }
            var result = await _adService.GetListOfAdvertisingByTokenAsync(token);

            return Ok(result);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AdvertisingBase>> Get(int id, string token)
        {
            if (id <= 0 || string.IsNullOrWhiteSpace(token))
            {
                return BadRequest(ResponseMessages.NotAllParametersSpecified);
            }

            var responce = await _adService.GetAdvertisingByIdAndTokenAsync(id, token);

            if (responce == null)
            {
                return NotFound();
            }

            return Ok(responce);
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
