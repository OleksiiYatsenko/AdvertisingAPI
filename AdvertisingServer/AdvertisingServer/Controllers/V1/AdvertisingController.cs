using AdvertisingServer.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AdvertisingServer.Constants;
using AdvertisingServer.Models.Dto.Advertising;
using AdvertisingServer.Infrastructure.Base;

namespace AdvertisingServer.Controllers.V1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class AdvertisingController : BaseMarketingController
    {
        private readonly IAdvertisingService _adService;

        public AdvertisingController(IAdvertisingService advertisingService) : base()
        {
            _adService = advertisingService;
        }
        
        [HttpGet]
        [SwaggerOperation(nameof(GetAll))]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(AdvertisingBase), "Returns all yours advertising")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, description: "Returns 400 if not all parameters were specified")]
        public async Task<ActionResult<IEnumerable<AdvertisingBase>>> GetAll(string token)
        {
            if(CheckValue(token, nameof(token)))
            {
                return BadRequest(string.Format(ResponseMessages.NotAllParametersSpecified, Container.ToString()));
            }

            var result = await _adService.GetListOfAdvertisingByTokenAsync(token);

            return Ok(result);
        }
        
        [HttpGet("{id}")]
        [SwaggerOperation(nameof(GetById))]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(AdvertisingBase), "Returns all yours advertising")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, description: "Resturn 404 status elements not found")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, description: "Returns 400 status when invalid parameters provided")]
        public async Task<ActionResult<AdvertisingBase>> GetById(int id, string token)
        {
            if (CheckValue(id, nameof(id)) || CheckValue(token, nameof(token)))
            {
                return BadRequest(string.Format(ResponseMessages.NotAllParametersSpecified, Container.ToString()));
            }

            var responce = await _adService.GetAdvertisingByIdAndTokenAsync(id, token);

            if (responce == null)
            {
                return NotFound();
            }

            return Ok(responce);
        }
        
        [HttpPost]
        [SwaggerOperation(nameof(Create))]
        [SwaggerResponse((int)HttpStatusCode.Created, typeof(AdvertisingBase), "Returns status code if entityt created successfully")]
        public async Task<IActionResult> Create([FromBody] AdvertisingBase upsertRequest)
        {
            upsertRequest.AdvertisingId = 0;
            var responce = await _adService.AddAdvertisingAsync(upsertRequest);
            return CreatedAtRoute(nameof(GetById), new { id = responce.AdvertisingId }, responce);
        }
        
        [HttpPut("{id}")]
        [SwaggerOperation(nameof(Update))]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(AdvertisingBase), "Returns status code if entityt created successfully")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, description: "Resturn 404 status elements not found")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, description: "Returns 400 if not all parameters were specified")]
        public async Task<IActionResult> Update(int id, [FromBody] AdvertisingBase upsertRequest)
        {
            if(CheckValue(id, nameof(id)))
            {
                return BadRequest(string.Format(ResponseMessages.NotAllParametersSpecified, Container.ToString()));
            }

            var advertising = await _adService.GetAdvertisingByIdAndTokenAsync(id, upsertRequest.Token);

            if(advertising == null)
            {
                return NotFound();
            }

            upsertRequest.AdvertisingId = id;

            await _adService.UpdateAdvertisingAsync(upsertRequest);

            return Ok();
        }
        
        [HttpDelete("{id}")]
        [SwaggerOperation(nameof(Delete))]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(AdvertisingBase), "Returns status code if entityt created successfully")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, description: "Resturn 404 status elements not found")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, description: "Returns 400 if not all parameters were specified")]
        public async Task<IActionResult> Delete(int id, string token)
        {
            if(CheckValue(id, nameof(id)) || CheckValue(token, nameof(token)))
            {
                return BadRequest(string.Format(ResponseMessages.NotAllParametersSpecified, Container.ToString()));
            }

            var advertising = _adService.GetAdvertisingByIdAndTokenAsync(id, token);
            if(advertising == null)
            {
                return NotFound();
            }

            await _adService.DeleteAdvertisingAsync(id, token);

            return Ok();
        }
    }
}
