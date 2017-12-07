using System;
using CryptoNotifier.Common.HTTP;
using CryptoNotifier.Common.WS.Model;
using Newtonsoft.Json;
using WebSocket4Net;

namespace CryptoNotifier.Common.WS
{
    public class BitfinexWebSocket
    {
        WebSocket _ws;

        public BitfinexWebSocket(string key)
        {
            _ws = new WebSocket(BitfinexEndpoints.DEFAULT_BASE_URL);

            _ws.Opened += _ws_Opened;
            _ws.Error += _ws_Error;
            _ws.Closed += _ws_Closed;
            _ws.MessageReceived += _ws_MessageReceived;

            _ws.EnableAutoSendPing = true;
            _ws.AutoSendPingInterval = 5;

            _ws.Open();


        }

        void _ws_Opened(object sender, EventArgs e)
        {
            Console.WriteLine("WebSocket is Open");

            //NOTE: Safe to send commands now
            var ping = new Ping
            {
                Event = "ping"
            };

            var subscribe = new Subscribe
            {
                Event = "subscribe",
                Channel = "trades",
                Symbol = "tBTCEUR"
            };

            var json = JsonConvert.SerializeObject(subscribe);

            _ws.Send(json);
        }

        void _ws_MessageReceived(object sender, MessageReceivedEventArgs e)
        {
            Console.WriteLine(e.Message);
        }

        void _ws_Error(object sender, SuperSocket.ClientEngine.ErrorEventArgs e)
        {
            Console.WriteLine("Websocket received an Error");
        }

        void _ws_Closed(object sender, EventArgs e)
        {
            Console.WriteLine("Websocket is now Closed");
        }
    }
}
