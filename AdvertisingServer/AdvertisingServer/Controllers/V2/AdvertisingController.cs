using AdvertisingServer.Infrastructure.Base;
using AdvertisingServer.Infrastructure.ContractResolver;
using AdvertisingServer.Infrastructure.Interfaces;
using AdvertisingServer.Models.Constants;
using AdvertisingServer.Models.Dto.Advertising;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Annotations;

namespace AdvertisingServer.Controllers.V2
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("2.0")]
    public class AdvertisingController : BaseMarketingController
    {
        private readonly IAdvertisingService _service;

        public AdvertisingController(IAdvertisingService service)
        {
            _service = service;
        }

        // GET: api/Advertising
        [HttpGet]
        [SwaggerOperation(nameof(GetAdvertisings))]
        [SwaggerResponse((int)HttpStatusCode.OK, type: typeof(AdvertisingBase[]), description: "Successfully found advertisings")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Specified invalid parametes")]
        public async Task<JsonResult> GetAdvertisings(string token, [FromQuery(Name = "fields")]string[] fields)
        {
            if (CheckValue(token, nameof(token)))
            {
                return new JsonResult(string.Format(Messages.NotAllParametersSpecified, Container.ToString()))
                {
                    StatusCode = 404
                };
            }

            var contractResolver = fields.Length > 0 ? new CustomContractResolver {AllowList = fields} : new DefaultContractResolver();

            var result = await _service.GetListOfAdvertisingByTokenAsync(token);

            return new JsonResult(result, new JsonSerializerSettings
            {
                ContractResolver = contractResolver
            });
        }
    }
}
