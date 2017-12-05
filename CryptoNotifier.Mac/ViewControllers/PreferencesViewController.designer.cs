// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace CryptoNotifier.Mac.ViewControllers
{
	[Register ("PreferencesViewController")]
	partial class PreferencesViewController
	{
		[Outlet]
		AppKit.NSTextField APIKeyInput { get; set; }

		[Outlet]
		AppKit.NSTextField APISecretInput { get; set; }

		[Outlet]
		AppKit.NSButton FiveMinutes { get; set; }

		[Outlet]
		AppKit.NSButton FiveSecond { get; set; }

		[Outlet]
		AppKit.NSButton OneMinute { get; set; }

		[Outlet]
		AppKit.NSButton OpenAtStartupCheckBox { get; set; }

		[Outlet]
		AppKit.NSButton RefreshOff { get; set; }

		[Outlet]
		AppKit.NSButton TenMinutes { get; set; }

		[Outlet]
		AppKit.NSButton TenSecond { get; set; }

		[Outlet]
		AppKit.NSButton ThirtySecond { get; set; }

		[Outlet]
		AppKit.NSButton TwoHalfMinutes { get; set; }

		[Action ("FiveMinClick:")]
		partial void FiveMinClick (AppKit.NSButton sender);

		[Action ("FiveSecondsClick:")]
		partial void FiveSecondsClick (AppKit.NSButton sender);

		[Action ("OneMinClick:")]
		partial void OneMinClick (AppKit.NSButton sender);

		[Action ("OnSaveClicked:")]
		partial void OnSaveClicked (AppKit.NSButton sender);

		[Action ("RefreshOffClick:")]
		partial void RefreshOffClick (AppKit.NSButton sender);

		[Action ("TenMinClick:")]
		partial void TenMinClick (AppKit.NSButton sender);

		[Action ("TenSecondsClick:")]
		partial void TenSecondsClick (AppKit.NSButton sender);

		[Action ("ThirtySecondsClick:")]
		partial void ThirtySecondsClick (AppKit.NSButton sender);

		[Action ("TwoHalfMinClick:")]
		partial void TwoHalfMinClick (AppKit.NSButton sender);
		
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

			if (FiveMinutes != null) {
				FiveMinutes.Dispose ();
				FiveMinutes = null;
			}

			if (FiveSecond != null) {
				FiveSecond.Dispose ();
				FiveSecond = null;
			}

			if (OneMinute != null) {
				OneMinute.Dispose ();
				OneMinute = null;
			}

			if (OpenAtStartupCheckBox != null) {
				OpenAtStartupCheckBox.Dispose ();
				OpenAtStartupCheckBox = null;
			}

			if (RefreshOff != null) {
				RefreshOff.Dispose ();
				RefreshOff = null;
			}

			if (TenMinutes != null) {
				TenMinutes.Dispose ();
				TenMinutes = null;
			}

			if (TenSecond != null) {
				TenSecond.Dispose ();
				TenSecond = null;
			}

			if (ThirtySecond != null) {
				ThirtySecond.Dispose ();
				ThirtySecond = null;
			}

			if (TwoHalfMinutes != null) {
				TwoHalfMinutes.Dispose ();
				TwoHalfMinutes = null;
			}
		}
	}
}
