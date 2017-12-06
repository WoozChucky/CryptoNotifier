using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using AppKit;
using CryptoNotifier.Common;
using CryptoNotifier.Common.Exceptions;
using CryptoNotifier.Mac.Notifications;
using CryptoNotifier.Mac.UI;
using CryptoNotifier.Mac.ViewControllers;
using CryptoNotifier.Mac.ViewModel;
using Foundation;

namespace CryptoNotifier.Mac
{
    public partial class MainViewController : NSViewController
    {
        CoinbaseAPI api;
        Timer RefreshTimer;
        bool FirstRun = true;

        public MainViewController(IntPtr handle) : base(handle)
        {
            Crypto.Instance.Initialize();
            NSUserNotificationCenter.DefaultUserNotificationCenter.Delegate = new CryptoNotificationDelegate();
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
            LoadingIndicator.StopAnimation(this);

            //8xQqUBVQJgOOSlbt
            //6NLygrlOIG6f1NjiTxpFsS1FwgtijMLs

            try {
                api = new CoinbaseAPI();
                api.Provide(Crypto.Instance.Settings.API_Key, Crypto.Instance.Settings.API_Secret); 
                if (Crypto.Instance.Settings.RefreshRate > 0)
                {
                    RefreshTimer.Start();
                }
            } 
            catch(ArgumentNullException)
            {
                RefreshTimer.Stop();
                ShowPreferences();
            }
        }

        public override void ViewWillAppear()
        {
            base.ViewWillAppear();
        }

        public override void ViewDidDisappear()
        {
            base.ViewDidDisappear();
            NSApplication.SharedApplication.Terminate(this);
        }

        void OnRefreshElapsed(object source, ElapsedEventArgs args)
        {

            if(FirstRun)
            {
                FirstRun = false;
                RefreshTimer.Interval = Crypto.Instance.Settings.RefreshRate * 1000;
                BeginInvokeOnMainThread( async () =>
                {
                    try
                    {
                        LoadingIndicator.StartAnimation(this);
                        await LoadUserData();
                        await LoadAccountsData();
                    }
                    catch (CoinbaseTokenException)
                    {
                        FirstRun = true;
                        RefreshTimer.Stop();
                        ShowPreferences();
                    }
                });
            }
            else 
            {
                BeginInvokeOnMainThread(async () => {
                    try
                    {
                        LoadingIndicator.StartAnimation(this);
                        await LoadAccountsData();
                        Console.WriteLine("New Account Data at " + args.SignalTime.ToString());
                    }
                    catch (CoinbaseTokenException)
                    {
                        RefreshTimer.Stop();
                        ShowPreferences();
                    }

                });
            }
        }

        void ShowPreferences()
        {
            this.CreateAlert("Settings Update Required",
                             "Please provide your Coinbase API account details.\n\nThe settings can be found at coinbase.com/settings/api.",
                 () =>
                 {
                     var prefVC = new PreferencesViewController();
                     prefVC.OnPreferencesSaved += (sender, e) =>
                     {
                        
                         //Update API settings and restart refreshing
                         api.Provide(Crypto.Instance.Settings.API_Key, Crypto.Instance.Settings.API_Secret);
                         RefreshTimer.Interval = Crypto.Instance.Settings.RefreshRate * 1000;
                         if(Crypto.Instance.Settings.RefreshRate > 0)
                            RefreshTimer.Start();
                    };
                    prefVC.OnPreferencesIgnored += (sender, e) => ShowPreferences();
                    PresentViewControllerAsModalWindow(prefVC);
                 }, NSAlertStyle.Warning);
        }

        async partial void OnFetchButtonClick(NSButton sender)
        {
            

            /*
            {
                var notification = new NSUserNotification();
                // Add text and sound to the notification
                notification.Title = "CryptoNotifier - Price Alert";
                notification.InformativeText = "Add your task to your activity log";
                notification.Subtitle = "This is a subtitle";
                notification.SoundName = NSUserNotification.NSUserNotificationDefaultSoundName;
                notification.HasActionButton = true; // Show "close" and "show" buttons when the notification is displayed as an alert
                notification.ActionButtonTitle = "Details";
                NSUserNotificationCenter.DefaultUserNotificationCenter.DeliverNotification(notification);
            }
            */
            await LoadAccountsData();

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

                if (vm.Currency != primaryCurrency) //Ignore EUR / USD
                {
                    var rate = await api.GetCurrentMarketPriceAsync(vm.Currency, primaryCurrency);

                    var converted = float.Parse(vm.Amount) * float.Parse(rate.Ammount);

                    vm.Conversion = $"{converted.ToString("N2")} {primaryCurrency}";
                    vm.MarketValue = $"{rate.Ammount} {primaryCurrency}";
                }
                else
                {
                    vm.Conversion = $"{vm.Amount} {vm.Currency}";
                    vm.MarketValue = $"1.00 {vm.Currency}";
                }

                vmList.Add(vm);
            }

            LoadingIndicator.StopAnimation(this);

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
