using System;
using System.Configuration;
using Xunit;

namespace AdvertisingServer.IntegrationTests
{
    public abstract class BaseApiIntegrationTests
    {
        protected static readonly string BaseAddress = ConfigurationManager.AppSettings["ApiEndpointBaseAddress"];
    }
}
