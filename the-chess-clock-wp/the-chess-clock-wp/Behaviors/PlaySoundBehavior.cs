using Microsoft.Xaml.Interactivity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace the_chess_clock_wp.Behaviors
{

    public class PlaySoundBehavior : DependencyObject, IBehavior
    {
        private Button assotiatedObject;
        private MediaElement mediaElement;

        public DependencyObject AssociatedObject
        {
            get { return this.assotiatedObject; }
            set { this.assotiatedObject = (Button)value; }
        }

        public void Attach(DependencyObject associatedObject)
        {
            this.assotiatedObject = (Button)associatedObject;
            this.assotiatedObject.Click += OnButtonClick;
            this.mediaElement = new MediaElement();
            mediaElement.Volume = 1;
            mediaElement.AutoPlay = false;
            mediaElement.Source = new Uri("ms-appx:///Sounds/button_16.mp3");

            mediaElement.MediaOpened += mediaOpened;
        }

        private void mediaOpened(object sender, RoutedEventArgs e)
        {

        }

        void OnButtonClick(object sender, RoutedEventArgs e)
        {
            mediaElement.Stop();
            mediaElement.Play();
        }

        public void Detach()
        {
            this.assotiatedObject.Click -= OnButtonClick;
        }
    }
}
