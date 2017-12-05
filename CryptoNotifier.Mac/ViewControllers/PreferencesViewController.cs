using System;
using Foundation;
using AppKit;
using CryptoNotifier.Mac.UI;
using System.Collections.Generic;
using System.Linq;

namespace CryptoNotifier.Mac.ViewControllers
{
    public partial class PreferencesViewController : AppKit.NSViewController
    {
        public event EventHandler OnPreferencesSaved;

        List<NSButton> checkBoxList;

        #region Constructors

        // Called when created from unmanaged code
        public PreferencesViewController(IntPtr handle) : base(handle)
        {
            Initialize();
        }

        // Called when created directly from a XIB file
        [Export("initWithCoder:")]
        public PreferencesViewController(NSCoder coder) : base(coder)
        {
            Initialize();
        }

        // Call to load from the XIB/NIB file
        public PreferencesViewController() : base("PreferencesView", NSBundle.MainBundle)
        {
            Initialize();
        }

        public override void ViewDidAppear()
        {
            base.ViewDidAppear();
        }

        public override void ViewWillAppear()
        {
            base.ViewWillAppear();
            checkBoxList = new List<NSButton>
            { FiveSecond, TenSecond, ThirtySecond, OneMinute, TwoHalfMinutes, FiveMinutes, TenMinutes, RefreshOff };

            APIKeyInput.StringValue = Crypto.Instance.Settings.API_Key;
            APISecretInput.StringValue = Crypto.Instance.Settings.API_Secret;
            OpenAtStartupCheckBox.State = Crypto.Instance.Settings.AutoStart ? NSCellStateValue.On : NSCellStateValue.Off;
            //TODO: Rest of params
        }


        // Shared initialization code
        void Initialize()
        {
            Title = "Preferences";
        }

        //TODO: Refractor this spaguetti!
        partial void FiveSecondsClick(NSButton sender)
        {
            checkBoxList.Where(b => b.State == NSCellStateValue.On).ToList().ForEach((b) => b.State = NSCellStateValue.Off);
            FiveSecond.State = NSCellStateValue.On;
        }
        partial void TenSecondsClick(NSButton sender)
        {
            checkBoxList.Where(b => b.State == NSCellStateValue.On).ToList().ForEach((b) => b.State = NSCellStateValue.Off);
            TenSecond.State = NSCellStateValue.On;
        }
        partial void ThirtySecondsClick(NSButton sender)
        {
            checkBoxList.Where(b => b.State == NSCellStateValue.On).ToList().ForEach((b) => b.State = NSCellStateValue.Off);
            ThirtySecond.State = NSCellStateValue.On;
        }
        partial void OneMinClick(NSButton sender)
        {
            checkBoxList.Where(b => b.State == NSCellStateValue.On).ToList().ForEach((b) => b.State = NSCellStateValue.Off);
            OneMinute.State = NSCellStateValue.On;
        }
        partial void TwoHalfMinClick(NSButton sender)
        {
            checkBoxList.Where(b => b.State == NSCellStateValue.On).ToList().ForEach((b) => b.State = NSCellStateValue.Off);
            TwoHalfMinutes.State = NSCellStateValue.On;
        }
        partial void FiveMinClick(NSButton sender)
        {
            checkBoxList.Where(b => b.State == NSCellStateValue.On).ToList().ForEach((b) => b.State = NSCellStateValue.Off);
            FiveMinutes.State = NSCellStateValue.On;
        }
        partial void TenMinClick(NSButton sender)
        {
            checkBoxList.Where(b => b.State == NSCellStateValue.On).ToList().ForEach((b) => b.State = NSCellStateValue.Off);
            TenMinutes.State = NSCellStateValue.On;
        }
        partial void RefreshOffClick(NSButton sender)
        {
            checkBoxList.Where(b => b.State == NSCellStateValue.On).ToList().ForEach((b) => b.State = NSCellStateValue.Off);
            RefreshOff.State = NSCellStateValue.On;
        }

        #endregion

        partial void OnSaveClicked(NSButton sender)
        {
            if(checkBoxList.All(b => b.State == NSCellStateValue.Off) || checkBoxList.All(b => b.State == NSCellStateValue.On)
               || !checkBoxList.Any(b => b.State == NSCellStateValue.On))
            {
                this.CreateAlert("Please choose a valid refresh rate.", null, null, NSAlertStyle.Critical);
                return;
            }

            var refresh = float.Parse(checkBoxList.FirstOrDefault(b => b.State == NSCellStateValue.On).Identifier);
            var key = APIKeyInput.StringValue;
            var secret = APISecretInput.StringValue;
            var auto_startup = (OpenAtStartupCheckBox.State == NSCellStateValue.On) ? true : false;

            Crypto.Instance.Settings = new AppSettings
            {
                API_Key = key,
                API_Secret = secret,
                AutoStart = auto_startup,
                RefreshRate = refresh
            };

            this.CreateAlert("Settings updated.", null, () => {
                OnPreferencesSavedHandler(EventArgs.Empty);
                DismissViewController(this);
            });
        }

        protected virtual void OnPreferencesSavedHandler(EventArgs e)
        {
            OnPreferencesSaved?.Invoke(this, e);
        }

        //strongly typed view accessor
        public new PreferencesView View
        {
            get
            {
                return (PreferencesView)base.View;
            }
        }
    }
}
