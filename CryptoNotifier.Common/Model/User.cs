using RestSharp.Deserializers;

namespace CryptoNotifier.Common.Model
{
    public class User
    {
        [DeserializeAs(Name = "id")]
        public string Id { get; set; }
        [DeserializeAs(Name = "name")]
        public string Name { get; set; }
        [DeserializeAs(Name = "username")]
        public string Username { get; set; }
        [DeserializeAs(Name = "profile_location")]
        public string ProfileLocation { get; set; }
        [DeserializeAs(Name = "profile_bio")]
        public string ProfileBio { get; set; }
        [DeserializeAs(Name = "profile_url")]
        public string ProfileUrl { get; set; }
        [DeserializeAs(Name = "avatar_url")]
        public string AvatarUrl { get; set; }
        [DeserializeAs(Name = "resource")]
        public string Resource { get; set; }
        [DeserializeAs(Name = "resource_path")]
        public string ResourcePath { get; set; }
    }
}
