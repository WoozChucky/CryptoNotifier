using System;
using AppKit;

namespace CryptoNotifier.Mac.UI
{
    public static class AlertDialog
    {
        public static void CreateAlert(this NSViewController view, string title, string message, Action action, NSAlertStyle style = NSAlertStyle.Informational)
        {
            view.InvokeOnMainThread(() => {
                var alert = new NSAlert
                {
                    InformativeText = message ?? "",
                    AlertStyle = style,
                    MessageText = title
                };

                alert.BeginSheetForResponse(view.View.Window, (result) => {
                    if(action != null)
                        action();
                });
            });
        }
    }
}
