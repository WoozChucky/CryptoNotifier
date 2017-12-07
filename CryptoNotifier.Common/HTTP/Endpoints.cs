namespace CryptoNotifier.Common.HTTP
{
    public static class CoinbaseEndpoints
    {
        public const string DEFAULT_BASE_URL = "https://api.coinbase.com/";
        public const string DEFAULT_API_VERSION = "v2/";
        public const string DEFAULT_API_DATE = "2017-11-30";

        public const string User = "user";
        public const string Accounts = "accounts";
        public const string ExchangeRates = "exchange-rates";
        public const string SpotPrice = "prices/{origin}-{destination}/spot";
    }

    public static class BitfinexEndpoints
    {
        public const string DEFAULT_BASE_URL = "wss://api.bitfinex.com/ws/2";
    }
}
