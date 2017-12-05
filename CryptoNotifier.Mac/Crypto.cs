using System;
using Foundation;

namespace CryptoNotifier.Mac
{
    public class Crypto
    {
        static Crypto _instance;

        NSUserDefaults _defaults;
        AppSettings _settings;

        Crypto()
        {
            
        }

        internal AppSettings Settings 
        {
            get { return _settings; } 
            set 
            { 
                _settings = value; 
                _defaults.SetString(_settings.API_Key, DefaultKeys.API_KEY);
                _defaults.SetString(_settings.API_Secret, DefaultKeys.API_SECRET);
                _defaults.SetFloat(_settings.RefreshRate, DefaultKeys.REFRESH_RATE);
                _defaults.SetBool(_settings.AutoStart, DefaultKeys.AUTO_START);
            }
        }

        public static Crypto Instance 
        { 
            get
            {
                if (_instance == null) _instance = new Crypto();
                return _instance;
            }
         }

        internal void Initialize()
        {
            _defaults = NSUserDefaults.StandardUserDefaults;
            ReloadAppSettings();
        }

        internal void ReloadAppSettings()
        {
            _settings.API_Key = _defaults.StringForKey(DefaultKeys.API_KEY);
            _settings.API_Secret = _defaults.StringForKey(DefaultKeys.API_SECRET);
            _settings.RefreshRate = _defaults.FloatForKey(DefaultKeys.REFRESH_RATE);
            _settings.AutoStart = _defaults.BoolForKey(DefaultKeys.AUTO_START);
        }

    }
}
