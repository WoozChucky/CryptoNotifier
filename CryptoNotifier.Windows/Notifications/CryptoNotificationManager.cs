using System;
using Windows.UI.Notifications;
using Microsoft.Toolkit.Uwp.Notifications;
using Microsoft.QueryStringDotNET;

namespace CryptoNotifier.Windows.Notifications
{
    public class CryptoNotificationManager
    {
        public static void SendLocalNotification(string title, string message)
        {

            //VISUALS SETUP
            var bindingGeneric = new ToastBindingGeneric();
            bindingGeneric.Children.Add(new AdaptiveText()
            {
                Text = title
            });
            bindingGeneric.Children.Add(new AdaptiveText()
            {
                Text = message
            });

            var visual = new ToastVisual()
            {
                BindingGeneric = bindingGeneric
            };

            //ACTIONS SETUP

            var actions = new ToastActionsCustom();


            var more_info_button = new ToastButton("More Info",
                new QueryString()
                {
                    {"action", "view_details"}
                }
                .ToString())
                {
                    ActivationType = ToastActivationType.Foreground
                };


            actions.Buttons.Add(more_info_button);
            actions.Buttons.Add(new ToastButtonDismiss("Dismiss"));

            //CONTENT SETUP
            var content = new ToastContent()
            {
                Visual = visual,
                Actions = actions,
                Launch = new QueryString()
                {
                    {"action", "view_details"}
                }.ToString(),
                Scenario = ToastScenario.Alarm,
                ActivationType = ToastActivationType.Foreground,
            };

            //TOAST SETUP
            var toast = new ToastNotification(content.GetXml())
            {
                Tag = Guid.NewGuid().ToString()
            };

            ToastNotificationManager.CreateToastNotifier().Show(toast);

        }


    }
}
