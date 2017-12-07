using Newtonsoft.Json;

namespace CryptoNotifier.Common.WS.Model
{
    public class Ping
    {
        [JsonProperty("event")]
        public string Event { get; set; }
    }
}
