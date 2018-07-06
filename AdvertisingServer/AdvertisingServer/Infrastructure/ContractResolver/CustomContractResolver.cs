using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdvertisingServer.Infrastructure.ContractResolver
{
    public class CustomContractResolver : DefaultContractResolver
    {
        public string[] AllowList { get; set; }

        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            var properties = base.CreateProperties(type, memberSerialization);

            properties = properties.Where(p => AllowList.Contains(p.PropertyName)).ToList();

            return properties;
        }
    }
}
