using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using the_chess_clock_wp.ViewModel;
using the_chess_clock_wp.Views;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace the_chess_clock_wp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private ViewModelLocator vmLocator;

        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;
            this.vmLocator = new ViewModelLocator();            
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var settings = this.vmLocator.Settings;
            if (settings.Usages > 3 && !settings.Reviewed)
            {
                MessageDialog msgbox = new MessageDialog("We noticed you are using our app for some time. If you like it or you want it to be better please rate it!", "Rate Free Chess Clock app");
                //OK Button
                UICommand okBtn = new UICommand("OK");
                okBtn.Invoked = async (s) =>
                {
                    await Windows.System.Launcher.LaunchUriAsync(
                        new Uri("ms-windows-store:reviewapp?appid=0a42480d-2ff1-4bfa-aa53-1bfd38c67d22"));
                    settings.Reviewed = true;
                };
                msgbox.Commands.Add(okBtn);

                //Cancel Button
                UICommand cancelBtn = new UICommand("Cancel");
                msgbox.Commands.Add(cancelBtn);

                msgbox.DefaultCommandIndex = 0;
                msgbox.CancelCommandIndex = 1;

                //Show message
                msgbox.ShowAsync();
            }
        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Settings));
        }
    }
}
