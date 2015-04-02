using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using the_chess_clock_wp.Common;
#if SILVERLIGHT
    using the_chess_clock_wp.WP8.Common;
#endif

namespace the_chess_clock_wp.ViewModel
{
    public class SettingsViewModel : ViewModelBase
    {
        private const string BLACK_TIME = "blackTime";
        private const string WHITE_TIME = "whiteTIme";
        private const string INCREMENTAL = "incremental";
        private const string REVIEWED = "reviewed";
        private const string USAGES = "usages";

        private ISetttingsProvider settings;
        private RelayCommand addSecondCommand;
        private RelayCommand substractSecondCommand;

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

        public int Incremental
        {
            get
            {
                return this.settings.GetValueOrDefault<int>(INCREMENTAL, 0);
            }

            set
            {
                if (value >= 0)
                {
                    this.settings.SetValue(INCREMENTAL, value);
                    this.RaisePropertyChanged("Incremental");
                }
            }
        }

        public bool Reviewed
        {
            get
            {
                return this.settings.GetValueOrDefault<bool>(REVIEWED, false);

            }

            set
            {
                this.settings.SetValue(REVIEWED, value);
                this.RaisePropertyChanged("Reviewed");
            }
        }

        public int Usages
        {
            get
            {
                return this.settings.GetValueOrDefault<int>(USAGES, 0);
            }

            set
            {
                if (value >= 0)
                {
                    this.settings.SetValue(USAGES, value);
                    this.RaisePropertyChanged("Usages");
                }
            }
        }

        public ICommand AddSecondCommand
        {
            get
            {
                if (this.addSecondCommand == null)
                {
                    this.addSecondCommand = new RelayCommand(() => Incremental++ );
                }

                return this.addSecondCommand;
            }
        }

        public ICommand SubstractSecondCommand
        {
            get
            {
                if (this.substractSecondCommand == null)
                {
                    this.substractSecondCommand = new RelayCommand(() => Incremental--);
                }

                return this.substractSecondCommand;
            }
        }

        public List<int> IncrementalRange
        {
            get
            {
                return Enumerable.Range(0, 59).ToList();
            }
        }        
    }
}
