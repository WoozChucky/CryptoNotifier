using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using AppKit;

namespace CryptoNotifier.Mac.ViewControllers
{
    public partial class AlertsViewController : AppKit.NSViewController
    {
        #region Constructors

        // Called when created from unmanaged code
        public AlertsViewController(IntPtr handle) : base(handle)
        {
            Initialize();
        }

        // Called when created directly from a XIB file
        [Export("initWithCoder:")]
        public AlertsViewController(NSCoder coder) : base(coder)
        {
            Initialize();
        }

        // Call to load from the XIB/NIB file
        public AlertsViewController() : base("AlertsView", NSBundle.MainBundle)
        {
            Initialize();
        }

        // Shared initialization code
        void Initialize()
        {
        }

        #endregion

        //strongly typed view accessor
        public new AlertsView View
        {
            get
            {
                return (AlertsView)base.View;
            }
        }
    }
}
