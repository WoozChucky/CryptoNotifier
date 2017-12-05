using System;
using Foundation;

namespace CryptoNotifier.Mac.Notifications
{
    public class CryptoNotificationDelegate : NSUserNotificationCenterDelegate
    {

        public override bool ShouldPresentNotification(NSUserNotificationCenter center, NSUserNotification notification)
        {
            return true;
            //return base.ShouldPresentNotification(center, notification);
        }

        public override void DidActivateNotification(NSUserNotificationCenter center, NSUserNotification notification)
        {
            if (notification.ActivationType == NSUserNotificationActivationType.ActionButtonClicked 
                || notification.ActivationType == NSUserNotificationActivationType.ContentsClicked)
            {
                //TODO: Show UI
            }
        }
    }
}
