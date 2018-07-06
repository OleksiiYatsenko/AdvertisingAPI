using System;
using System.Collections.Generic;
using System.Text;
using AdvertisingServer.UnitTests.DataFixture;
using Xunit;

namespace AdvertisingServer.UnitTests
{
    public class AdvertisingTests : IClassFixture<AdvertisingDataFixture>
    {
        private readonly AdvertisingDataFixture _advertisingData;

        public AdvertisingTests(AdvertisingDataFixture advertisingData)
        {
            _advertisingData = advertisingData;
        }


    }
}
