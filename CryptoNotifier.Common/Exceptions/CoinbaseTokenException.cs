using System;
namespace CryptoNotifier.Common.Exceptions
{
    public class CoinbaseTokenException : Exception
    {
        public CoinbaseTokenException(string message) : base(message){}
    }
}
