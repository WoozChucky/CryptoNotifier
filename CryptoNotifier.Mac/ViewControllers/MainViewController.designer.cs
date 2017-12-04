// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace CryptoNotifier.Mac
{
	[Register ("MainViewController")]
	partial class MainViewController
	{
		[Outlet]
		AppKit.NSTableView AccountsTable { get; set; }

		[Outlet]
		AppKit.NSImageView AvatarImage { get; set; }

		[Outlet]
		AppKit.NSButton FetchButton { get; set; }

		[Outlet]
		AppKit.NSTextField NameTextField { get; set; }

		[Action ("OnFetchButtonClick:")]
		partial void OnFetchButtonClick (AppKit.NSButton sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (FetchButton != null) {
				FetchButton.Dispose ();
				FetchButton = null;
			}

			if (NameTextField != null) {
				NameTextField.Dispose ();
				NameTextField = null;
			}

			if (AvatarImage != null) {
				AvatarImage.Dispose ();
				AvatarImage = null;
			}

			if (AccountsTable != null) {
				AccountsTable.Dispose ();
				AccountsTable = null;
			}
		}
	}
}
