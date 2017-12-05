using AppKit;
using Foundation;

namespace CryptoNotifier.Mac
{
    [Register("AppDelegate")]
    public class AppDelegate : NSApplicationDelegate
    {

        public override void DidFinishLaunching(NSNotification notification)
        {
            // Insert code here to initialize your application

            NSApplication.SharedApplication.SetAutomaticCustomizeTouchBarMenuItemEnabled(true);
        }

        public override void WillTerminate(NSNotification notification)
        {
            // Insert code here to tear down your application
        }
    }
}
