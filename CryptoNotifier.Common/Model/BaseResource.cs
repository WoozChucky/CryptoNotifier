using System;
using RestSharp.Deserializers;

namespace CryptoNotifier.Common.Model
{
    public class BaseResource
    {
        [DeserializeAs(Name = "id")]
        public string Id { get; set; }

        [DeserializeAs(Name = "resource")]
        public string Resource { get; set; }

        [DeserializeAs(Name = "resource_path")]
        public string ResourcePath { get; set; }
    }
}
