using System;
using Newtonsoft.Json;

namespace CryptoNotifier.Common.WS.Model
{
    public class Subscribe
    {
        [JsonProperty("event")]
        public string Event { get; set; }

        [JsonProperty("channel")]
        public string Channel { get; set; }

        [JsonProperty("symbol")]
        public string Symbol { get; set; }
    }
}
