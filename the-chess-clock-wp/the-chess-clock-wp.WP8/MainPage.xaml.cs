using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using the_chess_clock_wp.WP8.Resources;
using the_chess_clock_wp.ViewModel;

namespace the_chess_clock_wp.WP8
{
    public partial class MainPage : PhoneApplicationPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            var vm = this.DataContext as ClockViewModel;
            if (vm != null)
            {
                vm.ResetTimeCommand.Execute(null);
            }
        }

        private void PlayButton_Click(object sender, EventArgs e)
        {
            var vm = this.DataContext as ClockViewModel;
            if (vm != null)
            {
                vm.StartTimeCommand.Execute(null);
            }
        }

        private void PauseButton_Click(object sender, EventArgs e)
        {
            var vm = this.DataContext as ClockViewModel;
            if (vm != null)
            {
                vm.PauseTimeCommand.Execute(null);
            }
        }

        private void SettingsButton_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/Settings.xaml", UriKind.Relative));
        }
    }
}