using GalaSoft.MvvmLight;
using System;
using the_chess_clock_wp.Common;

namespace the_chess_clock_wp.ViewModel
{
    public class SettingsViewModel : ViewModelBase
    {
        private const string BLACK_TIME = "blackTime";
        private const string WHITE_TIME = "whiteTIme";

        private ISetttingsProvider settings;

        public SettingsViewModel()
        {
            this.settings = new AppSettings();
        }


        public TimeSpan WhiteTime
        {
            get 
            {
                return this.settings.GetValueOrDefault<TimeSpan>(WHITE_TIME, TimeSpan.FromMinutes(5)); 
            }

            set
            {
                this.settings.SetValue(WHITE_TIME, value);
                this.RaisePropertyChanged("WhiteTime");
            }
        }

        public TimeSpan BlackTime
        {
            get
            {
                return this.settings.GetValueOrDefault<TimeSpan>(BLACK_TIME, TimeSpan.FromMinutes(5));
            }

            set
            {
                this.settings.SetValue(BLACK_TIME, value);
                this.RaisePropertyChanged("BlackTime");
            }
        }

    }
}
