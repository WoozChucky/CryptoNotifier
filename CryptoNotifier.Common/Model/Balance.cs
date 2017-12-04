using RestSharp.Deserializers;

namespace CryptoNotifier.Common.Model
{
    public class Balance
    {
        [DeserializeAs(Name = "amount")]
        public string Ammount { get; set; }

        [DeserializeAs(Name = "currency")]
        public string Currency { get; set; }
    }
}