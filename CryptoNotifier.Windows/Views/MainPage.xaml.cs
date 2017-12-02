using CryptoNotifier.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Notifications;
using CryptoNotifier.Windows.Notifications;
using Microsoft.Toolkit.Uwp.UI.Extensions;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace CryptoNotifier.Windows
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        CoinbaseAPI api;

        public MainPage()
        {
            InitializeComponent();
            api = new CoinbaseAPI();

            RequestButton.Click += RequestButton_Click;
            ApplicationView.SetTitle(this, "Ta Certo");
        }

        private void RequestButton_Click(object sender, RoutedEventArgs e)
        {
            api.Provide("8xQqUBVQJgOOSlbt", "6NLygrlOIG6f1NjiTxpFsS1FwgtijMLs");
            var user = api.GetUser();

            CryptoNotificationManager.SendLocalNotification("Title", "Message");
        }
    }
}
