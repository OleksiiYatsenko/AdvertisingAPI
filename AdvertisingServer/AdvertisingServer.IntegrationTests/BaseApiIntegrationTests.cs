using AdvertisingServer.Models.Dto.Advertising;
using MyTested.HttpServer;
using Newtonsoft.Json;
using System.Configuration;
using System.Net.Http;
using Xunit;

namespace AdvertisingServer.IntegrationTests
{
    public abstract class BaseApiIntegrationTests
    {
        protected static readonly string BaseAddress = ConfigurationManager.AppSettings["ApiEndpointBaseAddress"];

        protected AdvertisingBase CreateAdvertising(string token)
        {
            AdvertisingBase result = null;

            MyHttpServer.WorkingRemotely(BaseAddress)
                .WithHttpRequestMessage(req => req
                    .WithMethod(HttpMethod.Post)
                    .WithRequestUri($"/api/v1/advertising")
                    .WithJsonContent(@"{
                                        ""advertisingId"": 0,
                                        ""token"": ""string""
                                       }"))
                .ShouldReturnHttpResponseMessage()
                .WithSuccessStatusCode()
                .AndAlso()
                .WithContent(content =>
                {
                    result = JsonConvert.DeserializeObject<AdvertisingBase>(content);
                });

            Assert.NotNull(result);

            return result;
        }

        protected AdvertisingBase GetAdvertisingById(int id, string token)
        {
            AdvertisingBase result = null;
            MyHttpServer.WorkingRemotely(BaseAddress)
                .WithHttpRequestMessage(req => req
                    .WithMethod(HttpMethod.Get)
                    .WithRequestUri($"/api/v1/Advertising/{id}?token={token}"))
                .ShouldReturnHttpResponseMessage()
                .WithSuccessStatusCode()
                .AndAlso()
                .WithContent(content =>
                {
                    result = JsonConvert.DeserializeObject<AdvertisingBase>(content);
                });

            Assert.NotNull(result);

            return result;
        }
    }
}
