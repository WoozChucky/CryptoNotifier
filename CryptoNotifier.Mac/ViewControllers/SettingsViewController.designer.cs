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
	[Register ("SettingsViewController")]
	partial class SettingsViewController
	{
		[Outlet]
		AppKit.NSTextField APIKeyInput { get; set; }

		[Outlet]
		AppKit.NSTextField APISecretInput { get; set; }

		[Outlet]
		AppKit.NSTextField RefreshRateInput { get; set; }

		[Action ("OnRefreshSlide:")]
		partial void OnRefreshSlide (AppKit.NSSliderCell sender);

		[Action ("OnSaveClicked:")]
		partial void OnSaveClicked (AppKit.NSButton sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (APIKeyInput != null) {
				APIKeyInput.Dispose ();
				APIKeyInput = null;
			}

			if (APISecretInput != null) {
				APISecretInput.Dispose ();
				APISecretInput = null;
			}

			if (RefreshRateInput != null) {
				RefreshRateInput.Dispose ();
				RefreshRateInput = null;
			}
		}
	}
}
