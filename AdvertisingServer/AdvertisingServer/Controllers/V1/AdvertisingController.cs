using AdvertisingServer.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using AdvertisingServer.Models.Dto.Advertising;
using AdvertisingServer.Infrastructure.Base;
using AdvertisingServer.Models.Constants;
using Swashbuckle.AspNetCore.Annotations;

namespace AdvertisingServer.Controllers.V1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class AdvertisingController : BaseMarketingController
    {
        private readonly IAdvertisingService _adService;

        public AdvertisingController(IAdvertisingService advertisingService)
        {
            _adService = advertisingService;
        }
        
        [HttpGet]
        [SwaggerOperation(nameof(GetAll))]
        [SwaggerResponse((int)HttpStatusCode.OK, type: typeof(AdvertisingBase[]), Description = "Successfully found advertisings")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, description: "Specified invalid parametes")]
        public async Task<ActionResult<AdvertisingBase[]>> GetAll(string token)
        {
            if (CheckValue(token, nameof(token)))
            {
                return BadRequest(string.Format(Messages.NotAllParametersSpecified, Container.ToString()));
            }

            var result = await _adService.GetListOfAdvertisingByTokenAsync(token);

            return Ok(result);
        }
        
        [HttpGet("{id}", Name = "GetAdvertising")]
        [SwaggerOperation("GetAdvertisingActionResult")]
        [SwaggerResponse((int)HttpStatusCode.OK, type: typeof(AdvertisingBase), description: "Successfully found advertising")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, description: "Advertising not found")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, description: "Invalid parameters were specified")]
        public async Task<ActionResult<AdvertisingBase>> GetById(int id, string token)
        {
            if (CheckValue(id, nameof(id)) | CheckValue(token, nameof(token)))
            {
                return BadRequest(string.Format(Messages.NotAllParametersSpecified, Container.ToString()));
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
        [SwaggerResponse((int)HttpStatusCode.Created, type: typeof(AdvertisingBase), description: "Advertising successfully created")]
        public async Task<ActionResult<AdvertisingBase>> Create([FromBody] AdvertisingBase upsertRequest)
        {
            upsertRequest.AdvertisingId = 0;
            var response = await _adService.AddAdvertisingAsync(upsertRequest);
            return CreatedAtRoute("GetAdvertising", new { id = response.AdvertisingId, token = response.Token }, response);
        }
        
        [HttpPut("{id}")]
        [SwaggerOperation(nameof(Update))]
        [SwaggerResponse((int)HttpStatusCode.OK, description: "Advertising successfully updated")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, description: "Advertising not found")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, description: "Invalid parameters were specified")]
        public async Task<IActionResult> Update(int id, [FromBody] AdvertisingBase upsertRequest)
        {
            if (CheckValue(id, nameof(id)))
            {
                return BadRequest(string.Format(Messages.NotAllParametersSpecified, Container.ToString()));
            }

            var advertising = await _adService.GetAdvertisingByIdAndTokenAsync(id, upsertRequest.Token);

            if (advertising == null)
            {
                return NotFound();
            }

            upsertRequest.AdvertisingId = id;

            await _adService.UpdateAdvertisingAsync(upsertRequest);

            return Ok();
        }
        
        [HttpDelete("{id}")]
        [SwaggerOperation(nameof(Delete))]
        [SwaggerResponse((int)HttpStatusCode.OK, description: "Avertising sucessfully deleted")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, description: "Advertising not found")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, description: "Invalid parameters were specified")]
        public async Task<IActionResult> Delete(int id, string token)
        {
            if (CheckValue(id, nameof(id)) || CheckValue(token, nameof(token)))
            {
                return BadRequest(string.Format(Messages.NotAllParametersSpecified, Container.ToString()));
            }

            var advertising = _adService.GetAdvertisingByIdAndTokenAsync(id, token);
            if (advertising == null)
            {
                return NotFound();
            }

            await _adService.DeleteAdvertisingAsync(id, token);

            return Ok();
        }
    }
}
