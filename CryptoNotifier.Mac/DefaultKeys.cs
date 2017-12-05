namespace CryptoNotifier.Mac
{
    public class DefaultKeys
    {
        public const string API_KEY = "api_key";
        public const string API_SECRET = "api_secret";
        public const string AUTO_START = "auto_start";
        public const string REFRESH_RATE = "refresh_rate";
    }

    struct AppSettings
    {
        public float RefreshRate;
        public string API_Key;
        public string API_Secret;
        public bool AutoStart;
    }
}
