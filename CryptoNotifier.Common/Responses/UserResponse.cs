using CryptoNotifier.Common.Model;
using RestSharp.Deserializers;

namespace CryptoNotifier.Common.Responses
{
    public class UserResponse
    {
        [DeserializeAs(Name = "data")]
        public User Data { get; set; }
    }
}
