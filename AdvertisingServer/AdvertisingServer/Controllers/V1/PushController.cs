using AdvertisingServer.Infrastructure.Base;
using AdvertisingServer.Infrastructure.Interfaces;
using AdvertisingServer.Models.Constants;
using AdvertisingServer.Models.Dto.Publishing;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Swashbuckle.AspNetCore.Annotations;

namespace AdvertisingServer.Controllers.V1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class PushController : BaseMarketingController
    {
        private readonly IPublishingService _service;

        public PushController(IPublishingService service)
        {
            _service = service;
        }
        
        [HttpGet]
        [SwaggerOperation(nameof(GetAll))]
        [SwaggerResponse((int)HttpStatusCode.OK, type: typeof(IEnumerable<PublishingBase>), description: "Successfully found publishings relayted to you")]
        public async Task<ActionResult<IEnumerable<PublishingBase>>> GetAll(string token)
        {
            if (CheckValue(token, nameof(token)))
            {
                return BadRequest(string.Format(Messages.NotAllParametersSpecified, Container.ToString()));
            }
            var response = await _service.GetPublishingAsync(token);

            return Ok(response);
        }
        
        [HttpGet("advertising/{advertisingId}")]
        [SwaggerOperation(nameof(GetByAdvertisingId))]
        [SwaggerResponse((int)HttpStatusCode.OK, type: typeof(IEnumerable<PublishingBase>), description: "Seccessfully found publishings relayted to you filtered by advertising")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, description: "Specified invalid parameters")]
        public async Task<ActionResult<IEnumerable<PublishingBase>>> GetByAdvertisingId(int advertisingId, string token)
        {
            if (CheckValue(advertisingId, nameof(advertisingId)) && CheckValue(token, nameof(token)))
            {
                return BadRequest(string.Format(Messages.NotAllParametersSpecified, Container.ToString()));
            }

            var response = await _service.GetPublishingByAdvertisingIdAsync(token, advertisingId);

            return Ok(response);
        }

        [HttpGet("channel/{channelId}")]
        [SwaggerOperation(nameof(GetByChannelId))]
        [SwaggerResponse((int)HttpStatusCode.OK, type: typeof(IEnumerable<PublishingBase>), description: "Seccessfully found publishings relayted to you filtered by channel")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, description: "Specified invalid parameters")]
        public async Task<ActionResult<IEnumerable<PublishingBase>>> GetByChannelId(int chennelId, string token)
        {
            if (CheckValue(chennelId, nameof(chennelId)) && CheckValue(token, nameof(token)))
            {
                return BadRequest(string.Format(Messages.NotAllParametersSpecified, Container.ToString()));
            }

            var response = await _service.GetPublishingByChannelIdAsync(token, chennelId);

            return Ok(response);
        }

        [HttpGet("advertising/{advertisingId}/channel/{channelId}")]
        [SwaggerOperation(nameof(GetPublishing))]
        [SwaggerResponse((int)HttpStatusCode.OK, type: typeof(PublishingBase), description: "Seccessfully found publishing")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, description: "Specified invalid parameters")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, description: "Publishing information not found")]
        public async Task<ActionResult<PublishingBase>> GetPublishing(int advertisingId, int chennelId, string token)
        {
            if (CheckValue(chennelId, nameof(chennelId)) && CheckValue(token, nameof(token)) && CheckValue(advertisingId, nameof(advertisingId)))
            {
                return BadRequest(string.Format(Messages.NotAllParametersSpecified, Container.ToString()));
            }

            var response = await _service.GetPublishingForAdvertisingByChannelIdAsync(advertisingId, token, chennelId);
            if (response == null)
            {
                return NotFound(Messages.NoEntitiesFoundWithSpecifiedCriteria);
            }

            return Ok(response);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(nameof(GetPublishingById))]
        [SwaggerResponse((int)HttpStatusCode.OK, type: typeof(PublishingBase), description: "Seccessfully found publishing")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, description: "Specified invalid parameters")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, description: "Publishing information not found")]
        public async Task<ActionResult<PublishingBase>> GetPublishingById(int id, string token)
        {
            if (CheckValue(id, nameof(id)) && CheckValue(token, nameof(token)))
            {
                return BadRequest(string.Format(Messages.NotAllParametersSpecified, Container.ToString()));
            }

            var response = await _service.GetPublishingById(id, token);
            if (response == null)
            {
                return NotFound(Messages.NoEntitiesFoundWithSpecifiedCriteria);
            }

            return Ok(response);
        }
        
        [HttpPost]
        [SwaggerOperation(nameof(Push))]
        [SwaggerResponse((int)HttpStatusCode.Created, type: typeof(PublishingBase), description: "Seccessfully published advertising")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, description: "Specified invalid parameters")]
        public async Task<ActionResult<PublishingBase>> Push([FromBody] PublishingBase request)
        {
            if (CheckValue(request.Token, "Token") && CheckValue(request.AdvertisingId, "advertising id") &&
                CheckValue(request.ChannelId, "Channel id"))
            {
                return BadRequest(string.Format(Messages.NotAllParametersSpecified, Container.ToString()));
            }

            request.PublishingId = 0;
            var response = await _service.PublishAdAsync(request);

            return CreatedAtRoute(nameof(GetPublishingById), new {id = response.PublishingId, token = response.Token},
                response);
        }
    }
}
