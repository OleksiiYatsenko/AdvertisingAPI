using AdvertisingServer.Infrastructure.Base;
using AdvertisingServer.Infrastructure.Interfaces;
using AdvertisingServer.Models.Constants;
using AdvertisingServer.Models.Dto.Channel;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace AdvertisingServer.Controllers.V1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class ChannelController : BaseMarketingController
    {
        private readonly IChannelService _channelService;

        public ChannelController(IChannelService service)
        {
            _channelService = service;
        }
        
        [HttpGet]
        [SwaggerOperation(nameof(GetAll))]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(IEnumerable<ChannelBase>), "Successfully found available channels")]
        public async Task<ActionResult<IEnumerable<ChannelBase>>> GetAll()
        {
            var result = await _channelService.GetChannelsAsync();
            return Ok(result);
        }
        
        [HttpGet("{id}")]
        [SwaggerOperation(nameof(GetById))]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(ChannelBase), "Successfully found channel")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, description: "Channel not found")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, description: "Invalid parameters were specified")]
        public async Task<ActionResult<ChannelBase>> GetById(int id)
        {
            if (CheckValue(id, nameof(id)))
            {
                return BadRequest(string.Format(Messages.NotAllParametersSpecified, Container.ToString()));
            }

            var result = await _channelService.GetChannelByIdAsync(id);
            if (result == null)
            {
                return NotFound(Messages.NoEntitiesFoundWithSpecifiedCriteria);
            }

            return Ok(result);
        }
        
        [HttpPost]
        [SwaggerOperation(nameof(Create))]
        [SwaggerResponse((int)HttpStatusCode.Created, typeof(ChannelBase), "Channel successfully created")]
        public async Task<ActionResult<ChannelBase>> Create([FromBody] ChannelBase request)
        {
            request.ChannelId = 0;
            var response = await _channelService.AddChannelAsync(request);
            return CreatedAtRoute(nameof(GetById), new {id = response.ChannelId}, response);
        }
        
        [HttpPut("{id}")]
        [SwaggerOperation(nameof(Update))]
        [SwaggerResponse((int)HttpStatusCode.OK, description: "Channel successfully updated")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, description: "Channel not found")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, description: "Invalid parameters were specified")]
        public async Task<IActionResult> Update(int id, [FromBody] ChannelBase request)
        {
            if (CheckValue(id, nameof(id)))
            {
                return BadRequest(string.Format(Messages.NotAllParametersSpecified, Container.ToString()));
            }

            var channel = await _channelService.GetChannelByIdAsync(id);
            if (channel == null)
            {
                return NotFound(string.Format(Messages.NoEntitiesFoundWithSpecifiedCriteria));
            }

            await _channelService.UpdateChannelAsync(request);

            return Ok();

        }
        
        [HttpDelete("{id}")]
        [SwaggerOperation(nameof(Delete))]
        [SwaggerResponse((int)HttpStatusCode.OK, description: "Channel successfully deleted")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, description: "Channel not found")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, description: "Invalid parameters were specified")]
        public async Task<IActionResult> Delete(int id)
        {
            if (CheckValue(id, nameof(id)))
            {
                return BadRequest(string.Format(Messages.NotAllParametersSpecified, Container.ToString()));
            }

            var channel = await _channelService.GetChannelByIdAsync(id);
            if (channel == null)
            {
                return NotFound(string.Format(Messages.NoEntitiesFoundWithSpecifiedCriteria));
            }

            await _channelService.DeleteChannelAsync(id);

            return Ok();
        }
    }
}
