using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using AppKit;

namespace CryptoNotifier.Mac.UI
{
    public partial class AccountTableCell : AppKit.NSView
    {
        #region Constructors

        // Called when created from unmanaged code
        public AccountTableCell(IntPtr handle) : base(handle)
        {
            Initialize();
        }

        // Called when created directly from a XIB file
        [Export("initWithCoder:")]
        public AccountTableCell(NSCoder coder) : base(coder)
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
