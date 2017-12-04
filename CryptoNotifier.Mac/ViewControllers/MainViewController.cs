using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using AppKit;
using CryptoNotifier.Common;
using CryptoNotifier.Common.Model;
using CryptoNotifier.Mac.UI;
using CryptoNotifier.Mac.ViewModel;
using Foundation;

namespace CryptoNotifier.Mac
{
    public partial class MainViewController : NSViewController
    {
        CoinbaseAPI api;
        Timer RefreshTimer;
        bool FirstRun = true;

        AppSettings settings = new AppSettings();

        public MainViewController(IntPtr handle) : base(handle)
        {
            RefreshTimer = new Timer
            {
                Interval = 100
            };
            RefreshTimer.Elapsed += OnRefreshElapsed;
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            Title = "CryptoNotifier";

            //8xQqUBVQJgOOSlbt
            //6NLygrlOIG6f1NjiTxpFsS1FwgtijMLs

            if(LoadDefaultSettings())
            {
                api = new CoinbaseAPI();
                //api.Provide(settings.API_Key, settings.API_Secret, Language.en_US);
                api.Provide("8xQqUBVQJgOOSlbt", "6NLygrlOIG6f1NjiTxpFsS1FwgtijMLs");
                RefreshTimer.Start();
            }
            else
            {
                //Open Preferences Dialog
                PerformSegue("preferences_segue", this);
            }
        }

        bool LoadDefaultSettings()
        {
            var defaults = NSUserDefaults.StandardUserDefaults;

            settings.API_Key = defaults.StringForKey(DefaultKeys.API_KEY);
            settings.API_Secret = defaults.StringForKey(DefaultKeys.API_SECRET);
            settings.Language = defaults.StringForKey(DefaultKeys.LANGUAGE);
            settings.RefreshRate = defaults.FloatForKey(DefaultKeys.REFRESH_RATE);

            return (!string.IsNullOrEmpty(settings.API_Key)
                     && !string.IsNullOrEmpty(settings.API_Secret)
                     //&& !string.IsNullOrEmpty(settings.Language)
                     && settings.RefreshRate > 4);
        }

        public override void ViewDidDisappear()
        {
            base.ViewDidDisappear();
            NSApplication.SharedApplication.Terminate(this);
        }

        public override void PrepareForSegue(NSStoryboardSegue segue, NSObject sender)
        {
            base.PrepareForSegue(segue, sender);
        }

        void OnRefreshElapsed(object source, ElapsedEventArgs args)
        {
            if(FirstRun)
            {
                FirstRun = false;
                RefreshTimer.Interval = settings.RefreshRate * 1000;
                BeginInvokeOnMainThread( async () => await LoadUserData());
            }
            BeginInvokeOnMainThread(async () => {
                await LoadAccountsData();
                Console.WriteLine("New Account Data at " + args.SignalTime.ToString());
            });
        }

        partial void OnFetchButtonClick(NSButton sender)
        {
            //await LoadUserData();
        }

        async Task LoadAccountsData()
        {
            var accounts = await api.GetAccountsAsync();

            var primaryCurrency = accounts.FirstOrDefault(a => a.Type == "fiat").Balance.Currency;

            var vmList = new List<AccountViewModel>();

            foreach (var account in accounts)
            {
                var vm = new AccountViewModel
                {
                    Id = account.Id,
                    Currency = account.Balance.Currency,
                    Amount = account.Balance.Ammount
                };
                var rate = await api.GetExchangeRateAsync(vm.Currency);

                var converted = float.Parse(vm.Amount) * float.Parse(rate.Rates.FirstOrDefault(r => r.Key == primaryCurrency).Value);

                vm.Conversion = $"{converted.ToString("N2")} {primaryCurrency}";

                vmList.Add(vm);
            }

            AccountsTable.DataSource = new AccountsTableDataSource(vmList.Count());
            AccountsTable.Delegate = new AccountsTableDelegate(vmList.OrderByDescending(a => a.Amount).ToList());
        }

        async Task LoadUserData()
        {
            var user = await api.GetUserAsync();
            NameTextField.StringValue = user.Name + " : " + user.Id;
            AvatarImage.Image = new NSImage(new NSUrl(user.AvatarUrl));
        }


        public override NSObject RepresentedObject
        {
            get
            {
                return base.RepresentedObject;
            }
            set
            {
                base.RepresentedObject = value;
                // Update the view, if already loaded.
            }
        }

    }
}
