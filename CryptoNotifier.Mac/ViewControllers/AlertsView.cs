using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using AppKit;

namespace CryptoNotifier.Mac.ViewControllers
{
    public partial class AlertsView : AppKit.NSView
    {
        #region Constructors

        // Called when created from unmanaged code
        public AlertsView(IntPtr handle) : base(handle)
        {
            Initialize();
        }

        // Called when created directly from a XIB file
        [Export("initWithCoder:")]
        public AlertsView(NSCoder coder) : base(coder)
        {
            Initialize();
        }

        // Shared initialization code
        void Initialize()
        {
        }

        #endregion
    }
}
